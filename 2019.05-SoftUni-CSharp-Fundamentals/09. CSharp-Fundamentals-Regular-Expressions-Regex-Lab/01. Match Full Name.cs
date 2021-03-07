using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Program
{
    class Program
    {
        static void Main()
        {
            string regex = @"\b(?<firstName>[A-Z][a-z]+) (?<lastName>[A-Z][a-z]+)\b";
            string names = Console.ReadLine();
            var matches = Regex.Matches(names, regex);
            foreach(Match match in matches)
            {
                Console.Write(match + " ");
            }
            Console.WriteLine();

        }
    }
}