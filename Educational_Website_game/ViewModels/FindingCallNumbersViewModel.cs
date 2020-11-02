using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryDeweyApp.ViewModels
{
    public class FindingCallNumbersViewModel
    {
        public string question { get; set; }
        public string topLevelAns { get; set; }
        public string secondLevelAns { get; set; }
        public string thirdLevelAns { get; set; } 
        public string topLevelUserAns { get; set; }
        public string secondLevelUserAns { get; set; }
        public string thirdLevelUserAns { get; set; }
        public int currentLevel { get; set; }
        public List<string> TopLevelQ { get; set; }
        public List<string> SecondLevelQ { get; set; }
        public List<string> ThirdLevelQ { get; set; }
        public List<string> questionsList { get; set; }
        public int Result { get; set; }
        public int TotalMarks { get; set; }
        public double Percentage { get; set; }
        public string TimeCompleted { get; set; }
        public TimeSpan TimeRunning { get; set; }
        public TimeSpan TimeSpanCompleted { get; set; }
        public List<TimeSpan> TimeRunningList { get; set; }
        public bool SavedResult { get; set; }
    }

}