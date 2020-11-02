using LibraryDeweyApp.Global;
using LibraryDeweyApp.Helpers;
using LibraryDeweyApp.Models;
using LibraryDeweyApp.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryDeweyApp.Controllers
{
    public class FindingCallNumbersController : Controller
    {
        // GET: FindingCallNumbers
        private ApplicationDbContext db = new ApplicationDbContext();
        private RandomGenerator rg = new RandomGenerator();

        public ActionResult Index()
        {
            //instantiate classes
            Random rand = new Random();
            CreateTree createTree = new CreateTree();
            FindNode node = new FindNode();
            FindingCallNumbersModel model = new FindingCallNumbersModel();
            FindingCallNumbersViewModel vm = new FindingCallNumbersViewModel();
            model.TopLevel = new List<string>();
            model.SecondLevel = new List<string>();
            model.ThirdLevel = new List<string>();
            //set question amount
            model.QuestionAmt = 4;

            //call method from Helper class to assign to Tree
            var tree = createTree.GetTree();

            //call method to assign model with values from tree
            tree.Nodes.ForEach(x => node.GetNode(x, 0, model));

            //instantiate view model
            vm.TopLevelQ = new List<string>();
            vm.SecondLevelQ = new List<string>();
            vm.ThirdLevelQ = new List<string>();
            vm.TimeRunningList = new List<TimeSpan>();

            //TOP LEVEL QUESTIONS
            //get 4 top-level questions
            List<int> randIndexes = new List<int>();
            //get list of random indexes that are unique
            randIndexes = rg.randomNumberList(0, 10, randIndexes, rand);
            //call method that returns random list of unique values
            vm.TopLevelQ = rg.RandomizeList(model.TopLevel, randIndexes);
            //shuffle list again to ensure randomization
            vm.TopLevelQ = vm.TopLevelQ.OrderBy(x => rand.Next()).Take(model.QuestionAmt).ToList();
            //get answer randomly
            vm.topLevelAns = vm.TopLevelQ[rand.Next(0, model.QuestionAmt)];
            //gets first char to get relevant children
            string identifier = vm.topLevelAns[0].ToString();

            //create random index to be the correct answer
            int randNum2 = rand.Next(0,model.QuestionAmt);
            int randNum3 = rand.Next(0,model.QuestionAmt);

      
            //SECOND LEVEL QUESTIONS
            //second level child questions
            vm.SecondLevelQ = model.SecondLevel.FindAll(x => x.StartsWith(identifier)).Take(model.QuestionAmt).ToList();
            //clear random number list
            randIndexes.Clear();
            //generate new random number list
            randIndexes = rg.randomNumberList(0, model.QuestionAmt, randIndexes, rand);
            //randomize list 
            vm.SecondLevelQ = rg.RandomizeList(vm.SecondLevelQ, randIndexes);
            //shuffle list again to ensure randomization
            vm.SecondLevelQ = vm.SecondLevelQ.OrderBy(x => rand.Next()).Take(model.QuestionAmt).ToList();
            //second level child random answer
            vm.secondLevelAns = vm.SecondLevelQ[randNum2].ToString();
            //get first two characters to get children of third level
            string secondLvlIdentifier = vm.secondLevelAns.Substring(0, 2);

            //THIRD LEVEL QUESTIONS
            //third level child questions
            vm.ThirdLevelQ = model.ThirdLevel.FindAll(x => x.StartsWith(secondLvlIdentifier)).Take(model.QuestionAmt).ToList();
            //clear random number list
            randIndexes.Clear();
            //generate new random number list
            randIndexes = rg.randomNumberList(0, model.QuestionAmt, randIndexes, rand);
            //randomize list 
            vm.ThirdLevelQ = rg.RandomizeList(vm.ThirdLevelQ, randIndexes);
            //shuffle questions for randomization
            vm.ThirdLevelQ = vm.ThirdLevelQ.OrderBy(x => rand.Next()).Take(model.QuestionAmt).ToList();
            //third level child random answer
            vm.thirdLevelAns = vm.ThirdLevelQ[randNum3].ToString();

            //now need to get only description part of call number to display as question
            vm.question = vm.thirdLevelAns.Substring(vm.thirdLevelAns.IndexOf(' ') + 1);

            //assign the current questions as top level questions
            vm.questionsList = vm.TopLevelQ;
            vm.currentLevel = 1;
            //assign total marks for test from leaderboard
            //get length of test from leaderboards
            var leaderboards = db.Leaderboards.Where(p => p.Name.Contains("Finding")).FirstOrDefault();
            vm.TotalMarks = leaderboards.TotalMarks;

            //assign tempdata to be accessed in other methods
            TempData["FindingCallNumbersModel"] = vm;
            
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string userAnswer, string TimeRunning)
        {
            //GET model using tempdata to avoid countless hidden fields
            FindingCallNumbersViewModel model = (FindingCallNumbersViewModel)TempData["FindingCallNumbersModel"];
            //initalize variables
            bool finalQ = false;
            bool ansIncorrect = false;

            //check if answers were added
            if (string.IsNullOrEmpty(userAnswer))
            {
                //viewbag, please select an answer!
                return View(model);
            }
            //return if null
            if (model == null)
            {
                return View();
            }

            //assign values from submission and assign to model
            model.TimeRunning = TimeSpan.Parse(TimeRunning);

            //check what level the questions are currently on
            //and add the user answers and next questions accordingly
            if (model.currentLevel == 1)
            {
               
                model.topLevelUserAns = userAnswer;
                if (userAnswer != model.topLevelAns)
                {
                    ansIncorrect = true;
                }

                model.questionsList = model.SecondLevelQ;        
                model.currentLevel = model.currentLevel + 1;
            }
            else if(model.currentLevel == 2)
            {
                model.secondLevelUserAns = userAnswer;
                if (userAnswer != model.secondLevelAns)
                {
                    ansIncorrect = true;
                }
                model.questionsList = model.ThirdLevelQ;    
                model.currentLevel = model.currentLevel + 1;
            }
            else
            {      
                //assign variable to know it is last iteration and save answers from model
                finalQ = true;
                model.thirdLevelUserAns = userAnswer;
            }
            //add running time of answer completion to list
            //this will ensure if website is slow, time taken to complete won't be affected
            model.TimeRunningList.Add(model.TimeRunning);
            //take all time values and add them for final time completion
            model.TimeCompleted = model.TimeRunningList.Aggregate((sum, val) => sum + val).ToString();
            //assign values to temp data again
            TempData["FindingCallNumbersModel"] = model;

            //if final quesiton, go to results
            if (finalQ == true || ansIncorrect)
            {
                return RedirectToAction("Save", "FindingCallNumbers");
            }

            return View(model);
        }

        public ActionResult HowTo()
        {
            return View();
        }

        public ActionResult Save()
        {
            //get model using tempdata
            FindingCallNumbersViewModel model = (FindingCallNumbersViewModel)TempData["FindingCallNumbersModel"];

       
            if (model == null)
            {
                return View(model);
            }

            //need to calculate user answers and assign to model result
            int count = 0;
            if (model.topLevelAns == model.topLevelUserAns)
            {
                count++;
            }
            if (model.secondLevelAns == model.secondLevelUserAns)
            {
                count++;
            }
            if(model.thirdLevelAns == model.thirdLevelUserAns)
            {
                count++;
            }

            model.Result = count;

            //calculate percentage and assign to model
            model.Percentage = (Convert.ToDouble(model.Result) / Convert.ToDouble(model.TotalMarks)) * 100;

            //assign tempdata to use in postback
            TempData["FindingCallNumbersModel"] = model;


            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Save(string x)
        {
            //get model using tempdata
            FindingCallNumbersViewModel model = (FindingCallNumbersViewModel)TempData["FindingCallNumbersModel"];

            //validate result
            if (model.Result != 0)
            {
                //check if they have previously saved an answer
                if (model.SavedResult != true)
                {
                    //instantiate model
                    Results result = new Results();

                    //get Leaderboard for current game
                    var leaderboards = db.Leaderboards.Where(p => p.Name.Contains("Finding")).FirstOrDefault();

                    //assign model values
                    result.UserID = HttpContext.User.Identity.GetUserId();
                    result.LeaderboardsID = leaderboards.LeaderboardsID;
                    result.result = model.Result;
                    result.DateCompleted = DateTime.Now;
                    result.TimeCompleted = model.TimeCompleted;

                    db.Results.Add(result);

                    var dbResult = db.SaveChanges();

                    model.SavedResult = true;

                    //check if db was successful
                    if (dbResult > 0)
                    {
                        TempData["SaveResult"] = $"Your Results were saved successfully on {DateTime.Now} !";
                    }
                    else
                    {
                        TempData["SaveResult"] = $"You dont have any results to save";
                    }
                }            
            }

            //assign tempdata to use in postback
            TempData["FindingCallNumbersModel"] = model;


            return View(model);
        }


    }
}