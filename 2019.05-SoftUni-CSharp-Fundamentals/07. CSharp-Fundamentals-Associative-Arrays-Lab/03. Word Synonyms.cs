using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            var dict = new Dictionary<string, List<string>>();

            for (int i = 0; i < n; i++)
            {
                string currentKey = Console.ReadLine();
                if(!dict.ContainsKey(currentKey))
                {
                    dict[currentKey] = new List<string>();
                }
                dict[currentKey].Add(Console.ReadLine());
            }

            foreach(var word in dict)
            {
                Console.Write($"{word.Key} - ");
                Console.Write(string.Join(", ", word.Value));
                Console.WriteLine();
            }
        }
    }
}