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
            string regex = @"\+359(\s|\-)2\1[0-9]{3}\1[0-9]{4}\b";
            string phones = Console.ReadLine();
            var matches = Regex.Matches(phones, regex);

            var phoneMatches = matches
                .Cast<Match>()
                .Select(a => a.Value.Trim())
                .ToList();
            var result = string.Join(", ", phoneMatches);
            Console.WriteLine(result);
        }
    }
}