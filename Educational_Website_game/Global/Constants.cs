using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryDeweyApp.Global
{
    public class Constants
    {
        //constants used for call numbers
        public double GeneralMax = 999.999;
        public double GeneralMin = 000.001;
        public int GeneralDecimal = 3;
        public int CutterLetterMax = 1;
        public int CutterMax = 999;
        public int CutterMin = 000;


        //constants for identifying areas
        //-----------------------------------
        public SortedDictionary<int, string> Categories = new SortedDictionary<int, string>(){ 
            { 000, "General Knowledge" },
            { 100, "Philosphy & Psychology"},
            { 200, "Religon"},
            { 300, "Social Sciences" },
            { 400, "Languages" },
            { 500, "Science" },
            { 600, "Technology" },
            { 700, "Arts & Recreation" },
            { 800, "Literature" },
            { 900, "History & Geography" }    
        };

        //max questions for match columns
        public int maxQuestions = 7;

    }
}