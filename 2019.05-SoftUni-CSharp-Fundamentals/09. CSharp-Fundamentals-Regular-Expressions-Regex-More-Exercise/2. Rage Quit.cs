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
            var input = Console.ReadLine();
            var strings = Regex.Split(input, @"[0-9]+")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(x => x.ToUpper())
                .ToList();
            var timesToRepeat = Regex.Split(input, @"[^0-9]+")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(int.Parse)
                .ToList();
            var result = new StringBuilder();
            var uniqueCount = new List<char>();
            for (int i = 0; i < strings.Count; i++)
            {
                int repeat = timesToRepeat[i];
                if(repeat == 0)
                {
                    strings.RemoveAt(i);
                    timesToRepeat.RemoveAt(i--);
                    continue;
                }
                for (int j = 0; j < repeat; j++)
                {
                    result.Append(strings[i]);
                }
            }
            //var iterateOver = strings.ToString();
            for (var i = 0; i < strings.Count; i++)
            {
                foreach (var letter in strings[i])
                {
                    if (!uniqueCount.Contains(letter))
                    {
                        uniqueCount.Add(letter);
                    }
                }
            }
            Console.WriteLine($"Unique symbols used: {uniqueCount.Count}\n" +
                $"{result}");
        }
    }
}