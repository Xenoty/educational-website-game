using LibraryDeweyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryDeweyApp.Global
{
    public class RandomGenerator
    {
        //returns a random call number 
        public string randCallNum(Random rand, Constants con)
        {
            double number1 = NextDouble(rand, con.GeneralMin, con.GeneralMax, con.GeneralDecimal);
            string cutter = RandCutter(rand, con.CutterLetterMax) + RandCutterNum(rand, con.CutterMin, con.CutterMax);
            string callNum = $"{number1} {cutter}";
            //con.CallNum = callNum;

            return callNum;
        }
        //returns a double with specified demical places
        public double NextDouble(Random rand, double min, double max, int decimalPlaces)
        {
            double randNum = rand.NextDouble() * (max - min) + min;
            return Convert.ToDouble(randNum.ToString("f" + decimalPlaces)); ;
        }
        //returns a random letter for the author intial
        public string RandCutter(Random rand, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rand.Next(s.Length)]).ToArray());
        }
        //returns a random int for number section of the cutter 000-999
        public int RandCutterNum(Random rand, int min, int max)
        {
            return rand.Next(min, max);
        }

        //return random list of int
        public List<int> randomNumberList(int startingNo, int amount, List<int> list, Random rand)
        {
            int randNumber;

            for (int i = startingNo; i < amount; i++)
            {
                do
                {
                    randNumber = rand.Next(amount);
                } while (list.Contains(randNumber));
                list.Add(randNumber);
            }

            return list;
        }

        //return list with randomly selected values
        public List<string> RandomizeList(List<string> list, List<int> randNumbers)
        {
            //initalize list
            List<string> randomizedList = new List<string>();
            //iterate through random numbers which don't repeat
            for (int i = 0; i < randNumbers.Count; i++)
            {
                for (int j = i; j < list.Count;)
                {
                    //add to new list
                    randomizedList.Add(list[randNumbers[j]].ToString());
                    break;
                }
            }

            return randomizedList;
        }
    }
}