using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryDeweyApp.Models
{
    public class Results
    {
        public int ResultsID { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int LeaderboardsID { get; set; }
        public virtual Leaderboards Leaderboards { get; set; }
        public int result { get; set; }
        public string TimeCompleted { get; set; }
        public DateTime DateCompleted { get; set; }
        
    }
}
