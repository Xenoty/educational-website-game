using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryDeweyApp.Models
{
    public class FindingCallNumbersModel
    {
       public string question { get; set; }
       public int QuestionAmt { get; set; }
       public List<string> TopLevel { get; set; }
       public List<string> SecondLevel { get; set; }
       public List<string> ThirdLevel{ get; set; }
    }
}