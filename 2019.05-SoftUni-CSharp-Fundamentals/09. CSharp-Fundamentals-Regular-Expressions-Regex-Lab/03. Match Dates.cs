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
            string regex = @"\b(?<day>\d{2})(?<separator>\.|\-|\/)(?<month>[A-z]{3})\2(?<year>\d{4})\b";
            string dates = Console.ReadLine();
            var matches = Regex.Matches(dates, regex);

            foreach(Match match in matches)
            {
                var day = match.Groups["day"].Value;
                var month = match.Groups["month"].Value;
                var year = match.Groups["year"].Value;

                Console.WriteLine($"Day: {day}, Month: {month}, Year: {year}");
            }
        }
    }
}