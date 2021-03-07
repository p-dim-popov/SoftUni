using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    class Program
    {
        static void Main()
        {
            var numbersWithLetters = Console.ReadLine()
                .Split(new char[] { ' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
            var listToSum = new List<decimal>();
            for (int i = 0; i < numbersWithLetters.Length; i++)
            {
                numbersWithLetters[i] = PerformedOperationsOn(numbersWithLetters[i]);
                decimal number = decimal.Parse(numbersWithLetters[i].Substring(1, numbersWithLetters[i].Length - 2));
                listToSum.Add(number);
            }
            Console.WriteLine($"{listToSum.Sum():f2}");
        }
        static string PerformedOperationsOn(string theString)
        {
            char firstLetter = theString[0];
            char lastLetter = theString[theString.Length - 1];
            decimal number = decimal.Parse(theString.Substring(1, theString.Length - 2));
            decimal index = Char.ToUpper(firstLetter) - 64;

            if (Char.IsUpper(firstLetter))
            {
                number /= index;
            }
            else if (Char.IsLower(firstLetter))
            {
                number *= index;
            }
            index = Char.ToUpper(lastLetter) - 64;
            if (Char.IsUpper(lastLetter))
            {
                number -= index;
            }
            else if (Char.IsLower(lastLetter))
            {
                number += index;
            }

            theString = $"{firstLetter}{number}{lastLetter}";

            return theString;
        }
    }
}