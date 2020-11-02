using LibraryDeweyApp.Global;
using LibraryDeweyApp.Helpers;
using LibraryDeweyApp.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace LibraryDeweyApp.Controllers
{
    public class ReplacingBooksController : Controller
    {
        //instantiate all my required objects
        private ApplicationDbContext db = new ApplicationDbContext();
        private RandomGenerator rg = new RandomGenerator();
        private Random rand = new Random();
        private Constants con = new Constants();

        // GET: ReplacingBooks
        public ActionResult Index()
        {
            //using to check if there was a postback
            ViewBag.PostBack = "false";

            return View();
        }

        [HttpPost]
        public ActionResult Index(string x)
        {
            CallNumbers cn = new CallNumbers();
            
            //get length of test from leaderboards
            var leaderboards = db.Leaderboards.Where(p => p.Name.Contains("Replacing")).FirstOrDefault();
            int res = leaderboards.TotalMarks;

            //intialize global list and TimeSpan for timer
            cn.RandomCallNumberList = new List<RandomCallNumbers>();
            cn.TimeRunning = new TimeSpan(0,0,0);

            //loop through total marks to display appropiate amount of random cn
            for (int i = 0; i < res; i++)
            {
                RandomCallNumbers rcn = new RandomCallNumbers();
                //call RandomGenerator to get random cn
                string randCallNum = rg.randCallNum(rand, con);
                rcn.ID = i + 1;
                rcn.RandCallNumber = randCallNum;
                //add randcn to global list
                cn.RandomCallNumberList.Add(rcn);
            }

            //used to check if HttpPost request
            ViewBag.PostBack = "true";

            return View(cn);
        }

   

    }
}