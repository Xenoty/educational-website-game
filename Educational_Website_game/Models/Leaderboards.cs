using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryDeweyApp.Models
{
    public class Leaderboards
    {
        public int LeaderboardsID { get; set; }

        [Required(ErrorMessage ="Name of leaderboard cannot be empty")]
        [Display(Name = "Name of Leaderboard")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter a name between 3 and 50 characters long.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Total Marks cannot be empty")]
        [Display(Name = "Total points from 0 - 10")]
        [Range(1,10, ErrorMessage = "Please enter a digit between 1 - 10")]
        public int TotalMarks { get; set; }
    }
}