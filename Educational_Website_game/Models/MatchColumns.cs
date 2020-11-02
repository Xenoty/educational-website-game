using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryDeweyApp.Models
{
    public class MatchColumns
    {
        [Display(Name = "Categories")]
        public SortedDictionary<int, string> categories { get; set; }

        //public virtual DictionaryModel DictionaryModel { get; set; }
        public List<int> RandomNoList { get; set; }

        [Display(Name = "Random Columns")]
        public Dictionary<int,string> questions { get; set; }

        [Display(Name = "Correct Columns")]
        public Dictionary<int, string> sortedQuestions { get; set; } 

        [Display(Name = "Your Columns")]
        public Dictionary<int, string> answers { get; set; } 
        
        //[Display(Name = "Macthed Columns")]
        //public SortedDictionary<int, string> matched { get; set; }
        public bool isCallNumberOrder { get; set; }
        public int Result { get; set; }
        public int TotalResult { get; set; }
        public string TimeCompleted { get; set; }
        public TimeSpan TimeRunning { get; set; }
        public bool SavedResult { get; set; }
    }
}