using LibraryDeweyApp.Global;
using LibraryDeweyApp.Helpers;
using LibraryDeweyApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryDeweyApp.Controllers
{
    public class ReplacingBooksResultController : Controller
    {
        //instatiate context
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        public ActionResult Index()
        {
            CallNumbers cn = TempData["model"] as CallNumbers;
            if (cn != null)
            {
                //return total matching values by counting matched values in new list
                int totalMatched = cn.MatchedCallNumbersList.Count();
                //total mark allocation
                ViewBag.TotalMarks = cn.SortedCallNumberList.Count();
                //percentage value
                double res = (Convert.ToDouble(totalMatched) / Convert.ToDouble(cn.SortedCallNumberList.Count())) * 100;
                ViewBag.Percent = res;
            }
            return View(cn);
        }
        // GET: ReplacingBooksResult
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CallNumbers cn, string answers)
        {

            //see if the user submitted anything
            if (!string.IsNullOrEmpty(answers))
            {
                int totalMatched = 0;
               
                LinqSort sort = new LinqSort();
                //now sort the list and compare
                List<string> randList = new List<string>();
                List<string> sortedList = new List<string>();

                //add cn.RandomCallNumberList call numbers to list<string> 
                foreach (var item in cn.RandomCallNumberList)
                {
                    randList.Add(item.RandCallNumber);
                }

                sortedList = sort.ReturnSortedList(randList);

                //split user's ansers and assign to list
                List<string> userAnswers = answers.Split(',').Select(sValue => sValue.Trim()).ToList();

                //initialize model lists
                cn.SortedCallNumberList = new List<SortedCallNumbers>();
                cn.UserCallNumberList = new List<UserCallNumbers>();
                cn.MatchedCallNumbersList = new List<MatchedCallNumbers>();

                for (int i = 0; i < sortedList.Count; i++)
                {
                    for (int j = i; j < userAnswers.Count;)
                    {
                        //used for id
                        int count = i + 1;

                        SortedCallNumbers scn = new SortedCallNumbers();
                        scn.ID = count;
                        scn.SortedCallNumber = sortedList[i];
                        cn.SortedCallNumberList.Add(scn);

                        UserCallNumbers ucn = new UserCallNumbers();
                        ucn.ID = count;
                        ucn.UserCallNumber = userAnswers[j];
                        cn.UserCallNumberList.Add(ucn);

                        break;
                    }          
                }



                //compare the sorted list and user answers
                MatchingLists ml = new MatchingLists();
                List<string> temp = new List<string>();
                temp = ml.MatchLists(sortedList, userAnswers);
                for (int i = 0; i < temp.Count; i++)
                {
                    MatchedCallNumbers mcn = new MatchedCallNumbers();
                    mcn.ID = i + 1;
                    mcn.MatchedCallNumber = temp[i];
                    cn.MatchedCallNumbersList.Add(mcn);
                }
                //cn.MatchedList = ml.MatchLists(sortedList, userAnswers);

                //return total matching values by counting matched values in new list
                totalMatched = cn.MatchedCallNumbersList.Count();

                //display results
                cn.Result = totalMatched;
                //total mark allocation
                ViewBag.TotalMarks = cn.SortedCallNumberList.Count();
                //percentage value
                double res = (Convert.ToDouble(totalMatched) / Convert.ToDouble(cn.SortedCallNumberList.Count())) * 100;
                ViewBag.Percent = res;
            }
            else
            {
                //asign viewbag for error?
                return RedirectToAction("Index");
            }

            //if (TempData["SaveResult"] != null)
            //{
            //    ViewBag.SaveResult = TempData["SaveResult"].ToString();
            //}
          
            return View(cn);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Save(CallNumbers cn)
        {
            //check if user entered any answers
            if (!string.IsNullOrEmpty(Convert.ToString(cn.Result)))
            {
                //instantiate model
                Results result = new Results();

                //get Leaderboard for current game
                var leaderboards = db.Leaderboards.Where(p => p.Name.Contains("Replacing")).FirstOrDefault();

                //assign model values
                result.UserID = HttpContext.User.Identity.GetUserId();
                result.LeaderboardsID = leaderboards.LeaderboardsID;
                result.result = cn.Result;
                result.DateCompleted = DateTime.Now;
                result.TimeCompleted = cn.TimeCompleted;

                db.Results.Add(result);

                var dbResult = db.SaveChanges();

                cn.SavedResult = true;

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

            TempData["model"] = cn;

            return RedirectToAction("Index");
            //return View();
        }
    }
}