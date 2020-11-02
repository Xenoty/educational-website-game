using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryDeweyApp.Models
{
    public class LeaderResults
    {
        public int LeaderResultsID { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int LeaderboardsID { get; set; }
        public virtual Leaderboards Leaderboards { get; set; }

    }
}