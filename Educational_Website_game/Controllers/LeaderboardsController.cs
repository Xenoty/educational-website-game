using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using LibraryDeweyApp.Models;
using LibraryDeweyApp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LibraryDeweyApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LeaderboardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Leaderboards
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Leaderboards.ToList());
        }

        // GET: Leaderboards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leaderboards leaderboards = db.Leaderboards.Find(id);
            if (leaderboards == null)
            {
                return HttpNotFound();
            }
            return View(leaderboards);
        }

        // GET: Leaderboards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Leaderboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeaderboardsID,Name,TotalMarks")] Leaderboards leaderboards)
        {
            if (ModelState.IsValid)
            {
                db.Leaderboards.Add(leaderboards);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leaderboards);
        }

        // GET: Leaderboards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leaderboards leaderboards = db.Leaderboards.Find(id);
            if (leaderboards == null)
            {
                return HttpNotFound();
            }
            return View(leaderboards);
        }

        // POST: Leaderboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LeaderboardsID,Name,TotalMarks")] Leaderboards leaderboards)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leaderboards).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leaderboards);
        }

        // GET: Leaderboards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leaderboards leaderboards = db.Leaderboards.Find(id);
            if (leaderboards == null)
            {
                return HttpNotFound();
            }
            return View(leaderboards);
        }

        // POST: Leaderboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Leaderboards leaderboards = db.Leaderboards.Find(id);
            db.Leaderboards.Remove(leaderboards);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        //custom method to view results of specific leaderboard
        public ActionResult Results(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //get leaderboard model by id
            Leaderboards leaderboards = db.Leaderboards.Find(id);

            if (leaderboards == null)
            {
                return HttpNotFound();
            }
            //getting identiy context
            var context = new IdentityDbContext<ApplicationUser>();
            var users = context.Users.ToList();

            //instantiate models
            List<ResultsVM> resultvm = new List<ResultsVM>();
            List<Results> result = new List<Results>();

            //return query result list and assign to list of results
            result = db.Results.SqlQuery(
                "Select *  " +
                "from Results r " +
                "Inner JOIN Leaderboards x ON r.LeaderboardsID = x.LeaderboardsID " +
                $"Where r.LeaderboardsID = {id}" +
                "Order by r.result DESC, r.TimeCompleted ASC;").ToList();

            //linq to add each item from my list of results into the viewmodel
            //Also asinging the the user details to the vm where userid = userid
            var resultvm2 = result.Select(x => new ResultsVM
            {
                ResultID = x.ResultsID,
                UserID = x.UserID,
                result = x.result,
                TimeCompleted = x.TimeCompleted,
                DateCompleted = x.DateCompleted,
                FirstName = users.First(y => y.Id == x.UserID).FirstName,
                LastName = users.First(y => y.Id == x.UserID).LastName
            }).ToList();

            //creating viewbags for custom use
            ViewBag.ldrName = leaderboards.Name;
            ViewBag.Id = id;

            //INGORE THIS, USING FOR FURTURE REFERENCE
            ////iterate through each result and add to viewmodel
            //for (int i = 0; i < result.Count; i++)
            //{
            //    ResultsVM rvm = new ResultsVM();

            //    rvm.ResultID = result[i].ResultsID;
            //    rvm.UserID = result[i].UserID;
            //    rvm.result = result[i].result;
            //    rvm.TimeCompleted = result[i].TimeCompleted;
            //    rvm.DateCompleted = result[i].DateCompleted;

            //    //iterate through each userid, if == then add details to vm
            //    for (int j = 0; j < users.Count; j++)
            //    {
            //        if (result[i].UserID == users[j].Id)
            //        {
            //            rvm.FirstName = users[j].FirstName;
            //            rvm.LastName = users[j].LastName;
            //        }
            //    }
            //    //add viewmodel to list
            //    resultvm.Add(rvm);
            //}

            //var test = db.Results.SqlQuery(
            //    "Select r.ResultsID, y.FirstName, y.LastName, r.result, r.TimeCompleted, r.DateCompleted  " +
            //    "from Results r " +
            //    "Inner JOIN Leaderboards x ON r.LeaderboardsID = x.LeaderboardsID " +
            //    "Inner JOIN AspNetUsers y ON r.UserID = y.Id " +
            //    $"Where r.LeaderboardsID = {id}" +
            //    "Order by r.TimeCompleted DESC, r.result DESC;").ToList();

            //IdentityDbContext<ApplicationUser> identityDb = new IdentityDbContext<ApplicationUser>();
            ////var query = db.Results.Select(ldr => ldr.LeaderboardsID == leaderboards.LeaderboardsID).ToList();

            //var query = (from r in db.Results
            //             join l in db.Leaderboards on r.LeaderboardsID equals l.LeaderboardsID
            //             join a in identityDb.Users on r.UserID equals a.Id
            //             where r.LeaderboardsID.Equals(id)
            //             select new
            //             {
            //                 ResultID = r.ResultsID,
            //                 FirstName = a.FirstName,
            //                 LastName = a.LastName,
            //                 result = r.result,
            //                 TimeCompleted = r.TimeCompleted,
            //                 DateCompleted = r.DateCompleted
            //             }).ToList();

            return View(resultvm2);
        }

        public ActionResult Clear(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //execute sql query to delete all results where ldrbrdId = ldrbrdId
            var temp = db.Database.ExecuteSqlCommand(
               $"DELETE FROM Results WHERE LeaderboardsID = '{id}';");
            db.SaveChanges();

            TempData["Result"] = $"Leaderboard successfully cleared";

            //return to the Results view
            return RedirectToAction("Results", "Leaderboards", new {id});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
