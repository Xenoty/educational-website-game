using LibraryDeweyApp.Global;
using LibraryDeweyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryDeweyApp.Controllers
{
    public class IdentifyAreasController : Controller
    {
        //instantiate all my required objects
        private ApplicationDbContext db = new ApplicationDbContext();
        private Random rand = new Random();
        private Constants con = new Constants();
        private RandomGenerator rg = new RandomGenerator();
        // GET: IndentifyAreas
        public ActionResult Index()
        {
            //using to check if there was a postback
            ViewBag.PostBack = "false";

            return View();
        }

        [HttpPost]
        public ActionResult Index(string y)
        {
            //instantiate model
            MatchColumns mc = new MatchColumns();

            //get length of test from leaderboards
            var leaderboards = db.Leaderboards.Where(p => p.Name.Contains("Identifying")).FirstOrDefault();
            int res = leaderboards.TotalMarks;

            //intialize global list and TimeSpan for timer
            mc.TimeRunning = new TimeSpan(0, 0, 0);

            //assign values to model
            mc.categories = con.Categories;
            mc.TotalResult = res;

            //intialize variables
            Dictionary<int, string> questionsDict = new Dictionary<int, string>();
            int correctAnswers = res;
            int totalCat = con.Categories.Count;
            int totalQuestions = con.maxQuestions;
            int diffBetweenNo = totalCat - totalQuestions;

            //need to first shuffle constant catergories
            //this is to make sure that the first 4 categories are rotated in different orders each time
            Dictionary<int, string> shuffle = mc.categories.OrderBy(x => rand.Next())
            .ToDictionary(item => item.Key, item => item.Value);

            //add to dictionary while loop according to total questions for game
            for (int i = 0; i < shuffle.Count - diffBetweenNo; i++)
            {
                questionsDict.Add(shuffle.ElementAt(i).Key, shuffle.ElementAt(i).Value);
            }
            //create random list of ints for shuffling indexes for questions
            List<int> randNoList = new List<int>();
            randNoList = rg.randomNumberList(0, questionsDict.Count, randNoList, rand);

            //assign dictionary to model
            mc.RandomNoList = randNoList;
            mc.questions = questionsDict;

            //generate random number between 0 - 1
            int randNum = rand.Next(0, 2);
            //check if 0 or 1 to alterante 
            if (randNum == 0)
            {
                //left column = call numbers
                mc.isCallNumberOrder = true;
                ViewBag.ColumnOrder = "Decimal Classes";
                ViewBag.OtherColumn = "Descriptions";
            }
            else
            {
                //left column = descriptions
                mc.isCallNumberOrder = false;
                ViewBag.ColumnOrder = "Descriptions";
                ViewBag.OtherColumn = "Decimal Classes";
            }


            //used to check if HttpPost request
            ViewBag.PostBack = "true";

            return View(mc);
        }
    }
}