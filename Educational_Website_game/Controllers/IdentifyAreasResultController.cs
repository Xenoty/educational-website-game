using LibraryDeweyApp.Global;
using LibraryDeweyApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryDeweyApp.Controllers
{
    public class IdentifyAreasResultController : Controller
    {
        // GET: IdentifyAreasResult
        //Instantiate variables
        Global.Constants con = new Global.Constants();
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            //get model through tempdata
            MatchColumns mc = TempData["model"] as MatchColumns;
            if (mc != null)
            {
                //total mark allocation
                ViewBag.TotalMarks = 4;
                //percentage value
                double res = (Convert.ToDouble(mc.Result) / Convert.ToDouble(mc.questions.Count())) * 100;
                ViewBag.Percent = res;
            }

            return View(mc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(MatchColumns mc, string questions, string orgAnswers, string answers)
        {

            //see if the user submitted anything
            if (!string.IsNullOrEmpty(answers))
            {
                //instantiate model paramaters 
                mc.answers = new Dictionary<int, string>();
                mc.questions = new Dictionary<int, string>();
                //split hidden fields to list<string>
                List<string> questionsList = questions.Split(',').Select(sValue => sValue.Trim()).ToList();
                List<string> originalAnswers = orgAnswers.Split(',').Select(sValue => sValue.Trim()).ToList();
                List<string> userAnswers = answers.Split(',').Select(sValue => sValue.Trim()).ToList();

                //create new dictionary answers 
                Dictionary<int, string> userAnswersDict = new Dictionary<int, string>();
                //intialize totalCorrect aswers from model
                int correctAnswers = mc.TotalResult;
                ViewBag.TotalMarks = correctAnswers;

                //loop through questions, answers and original answers to store to model
                //for next view
                for (int i = 0; i < questionsList.Count; i++)
                {
                    string item = questionsList[i].ToString();

                    if (i < correctAnswers + 1)
                    {
                        for (int j = i; j < userAnswers.Count;)
                        {
                            string ans = userAnswers[i].ToString();
                            //need to check if form collection is call number or description
                            if (mc.isCallNumberOrder == true)
                            {
                                //call number in left column
                                //item = call number 
                                userAnswersDict.Add(Convert.ToInt32(item), ans);
                                mc.answers.Add(Convert.ToInt32(item), ans);
                            }
                            else
                            {
                                //description in left column
                                //item = description
                                userAnswersDict.Add(Convert.ToInt32(ans), item);
                                mc.answers.Add(Convert.ToInt32(ans), item);
                            }
                            break;
                        }

                        //loop through orginal answers to add to model
                        for (int k = i; k < originalAnswers.Count;)
                        {
                            string org = originalAnswers[k].ToString();

                            if (mc.isCallNumberOrder == true)
                            {
                                //call number in left column
                                //item = call number 
                                mc.questions.Add(Convert.ToInt32(item), org);
                            }
                            else
                            {
                                //description in left column
                                //item = description
                                mc.questions.Add(Convert.ToInt32(org), item);
                            }
                            break;
                        }
                    }
                    else { break; }                           
                }

                //assign categories to model
                mc.categories = con.Categories;

                //still need to compare dictionaries for result
                //use linq to get values matching
                var matchingDict = mc.answers.Where(entry => mc.categories[entry.Key] == entry.Value)
                 .ToDictionary(entry => entry.Key, entry => entry.Value);

                //count total matching in new dictionary for result
                mc.Result = matchingDict.Count;
                double res = (Convert.ToDouble(mc.Result) / Convert.ToDouble(correctAnswers)) * 100;
                ViewBag.Percent = res;

                //need to get correct answers for left column
                //and store to sortedQuestions
                Dictionary<int, string> CorrectAnswerDict = new Dictionary<int, string>();

                for (int i = 0; i < mc.questions.Count; i++)
                {
                    //variables for easy access and typing
                    int qKey = mc.questions.ElementAt(i).Key;
                    string qValue = mc.questions.ElementAt(i).Value;

                    for (int j = 0; j < mc.categories.Count; j++)
                    {
                        //variables for easy access and 
                        int cKey = mc.categories.ElementAt(j).Key;
                        string cValue = mc.categories.ElementAt(j).Value;

                        //check if left column = description / callnumber using model
                        if (mc.isCallNumberOrder == true)
                        {
                            if (qKey == cKey)
                            {
                                //call number is question
                                CorrectAnswerDict.Add(qKey, cValue);
                                break;
                            }
                        }
                        else
                        {
                            if (qValue == cValue)
                            {
                                //description is question
                                CorrectAnswerDict.Add(cKey, cValue);
                                break;
                            }
                        }                 
                    }
                }

                //assign correct answer
                mc.sortedQuestions = CorrectAnswerDict;

                //may use for future reference below
                //var matches = mc.questions.Keys.Intersect(mc.categories.Keys);
                //foreach (var m in matches)
                //    ResultDictionary.Add(Convert.ToInt32(mc.questions[m]), mc.categories[m]);

                //assign model to tempdata as it is easier than using hidden fields
                TempData["IdentifyModel"] = mc;

                return View(mc);
            }
           

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Save()
        {
            //get model from tempdata[]
            MatchColumns mc = TempData["IdentifyModel"] as MatchColumns;
            //check if user entered any answers
            if (!string.IsNullOrEmpty(Convert.ToString(mc.Result)))
            {
                //instantiate model
                Results result = new Results();

                //get Leaderboard for current game
                var leaderboards = db.Leaderboards.Where(p => p.Name.Contains("Identifying")).FirstOrDefault();

                //assign model values
                result.UserID = HttpContext.User.Identity.GetUserId();
                result.LeaderboardsID = leaderboards.LeaderboardsID;
                result.result = mc.Result;
                result.DateCompleted = DateTime.Now;
                result.TimeCompleted = mc.TimeCompleted;

                db.Results.Add(result);

                var dbResult = db.SaveChanges();

                mc.SavedResult = true;

                //check if db was successful
                if (dbResult > 0)
                {
                    TempData["SaveResult"] = $"Your Results were saved successfully on {DateTime.Now} !";
                    //ViewBag.Success = $"Your Results were saved successfully on {DateTime.Now} !";
                }
                else
                {
                    TempData["SaveResult"] = $"You dont have any results to save";
                }
            }

            //assign model to new tempdata
            TempData["model"] = mc;

            return RedirectToAction("Index");
        }

    }
}