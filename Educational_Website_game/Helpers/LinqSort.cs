using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace LibraryDeweyApp.Global
{
    public class LinqSort
    {
        //sorting method by passing list as parameter
        public List<string> ReturnSortedList(List<string> list)
        {
            var result = list.OrderBy(x => PadNumbers(x));
            return result.ToList();
        }
        public string PadNumbers(string input)
        {
            return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
        }
    }
}