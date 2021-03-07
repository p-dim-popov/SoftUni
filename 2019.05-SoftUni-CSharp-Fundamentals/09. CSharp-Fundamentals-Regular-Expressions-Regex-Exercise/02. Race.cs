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
            var racerList = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .ToList();
            var racerDict = new Dictionary<string, int>();
            string input = Console.ReadLine();

            while (input != "end of race")
            {
                var nameRegex = new Regex(@"[A-Z]|[a-z]");
                var nameMatches = nameRegex.Matches(input);

                var placeRegex = new Regex(@"[0-9]");
                var placeMatches = placeRegex.Matches(input);

                var name = new StringBuilder();
                foreach (Match match in nameMatches)
                {
                    name.Append(match.Value);
                }
                if (racerList.Contains(name.ToString()))
                {
                    if(!racerDict.ContainsKey(name.ToString()))
                    {
                        racerDict[name.ToString()] = 0;
                    }
                    foreach (Match digit in placeMatches)
                    {
                        racerDict[name.ToString()] += int.Parse(digit.Value);
                    }
                }
                input = Console.ReadLine();
            }
            racerDict = racerDict
                .OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, y => y.Value);
            int a = 0;
            foreach (var kvp in racerDict)
            {
                racerList[a++] = kvp.Key;
            }
            Console.WriteLine($"1st place: {racerList[0]}");
            Console.WriteLine($"2nd place: {racerList[1]}");
            Console.WriteLine($"3rd place: {racerList[2]}");
        }
    }
}