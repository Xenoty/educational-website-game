using LibraryDeweyApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryDeweyApp.ViewModels
{
    public class ResultsVM
    {
        public int ResultID { get; set; }
        public string UserID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Result")]
        public int result { get; set; }
        [Display(Name = "Time")]
        public string TimeCompleted { get; set; }
        [Display(Name = "Date Completed")]
        public DateTime DateCompleted { get; set; }
    }
}