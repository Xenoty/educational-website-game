using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryDeweyApp.Helpers
{
    public class MatchingLists
    {
        //iterate through each value and if matching, add to new list
        //second for loop uses i as current array value, and does not increment
        //this is because we want to break after each check in the loop to keep the same order
        //example [0] = [0]? Then [1] = [1]?
        //This ensures the order is correct as well
        public List<string> MatchLists(List<string> arr1, List<string> arr2)
        {
            List<string> matchesList = new List<string>();

            for (int i = 0; i < arr1.Count; i++)
            {
                for (int j = i; j < arr2.Count;)
                {
                    if (arr1[i].ToString() == arr2[j].ToString())
                    {
                        matchesList.Add(arr1[i].ToString());
                    }
                    break;
                }

            }

            return matchesList;
        }
    }
}