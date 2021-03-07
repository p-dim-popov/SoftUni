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
            int n = int.Parse(Console.ReadLine());
            var attackedPlanets = new List<string>();
            var destroyedPlanets = new List<string>();

            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();
                var initRegex = new Regex(@"[sStTaArR]");
                var matches = initRegex.Matches(input);
                int key = matches.Count;
                var tempText = input.ToCharArray();
                for (int ch = 0; ch < input.Length; ch++)
                {
                    tempText[ch] = (char)(tempText[ch] - key);
                }
                string text = new string(tempText);

                var textRegex = new Regex(@"^.*\@(?<name>[a-zA-Z]+)[^\@\-\!\:\>]*\:(?<population>\d+)[^\@\-\!\:\>]*\!(?<action>[AD])\![^\@\-\!\:\>]*\-\>(?<soldiers>\d+)[^\@\-\!\:\>]*$");
                if (textRegex.IsMatch(text))
                {
                    var textMatch = textRegex.Match(text);
                    string name = textMatch.Groups["name"].Value;
                    int population = int.Parse(textMatch.Groups["population"].Value);
                    string action = textMatch.Groups["action"].Value;
                    int soldiers = int.Parse(textMatch.Groups["soldiers"].Value);

                    if (action == "A")
                    {
                        attackedPlanets.Add(name);
                    }
                    else if (action == "D")
                    {
                        destroyedPlanets.Add(name);
                    }
                }
            }
            Console.WriteLine($"Attacked planets: {attackedPlanets.Count}");
            attackedPlanets = attackedPlanets.OrderBy(x => x).ToList();
            attackedPlanets.ForEach(p => Console.WriteLine($"-> {p}"));
            Console.WriteLine($"Destroyed planets: {destroyedPlanets.Count}");
            destroyedPlanets = destroyedPlanets.OrderBy(x => x).ToList();
            destroyedPlanets.ForEach(p => Console.WriteLine($"-> {p}"));
        }
    }
}