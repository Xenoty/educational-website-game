using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryDeweyApp.Global
{
    public class CallNumbers
    {
        [Display(Name = "Random Call Numbers")]
        public List<RandomCallNumbers> RandomCallNumberList { get; set; }

        public virtual RandomCallNumbers RandomCallNumbers { get; set; }

        [Display(Name = "Sorted Call Numbers")]
        public List<SortedCallNumbers> SortedCallNumberList { get; set; }
        public virtual SortedCallNumbers SortedCallNumbers { get; set; }

        [Display(Name = "Your Call Numbers")]
        public List<UserCallNumbers> UserCallNumberList { get; set; }
        public virtual UserCallNumbers UserCallNumbers { get; set; }
        
        public List<MatchedCallNumbers> MatchedCallNumbersList { get; set; }
        public virtual MatchedCallNumbers MatchedCallNumbers { get; set; }
        //public List<string> MatchedList { get; set; }
        public int Result { get; set; }
        public string TimeCompleted { get; set; }
        public TimeSpan TimeRunning { get; set; }
        public bool SavedResult { get; set; }
      
    }
}