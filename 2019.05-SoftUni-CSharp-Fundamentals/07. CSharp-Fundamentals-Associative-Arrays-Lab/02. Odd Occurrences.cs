using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    class Program
    {
        static void Main()
        {
            var words = Console.ReadLine().Split();
            var dict = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!dict.ContainsKey(word.ToLower()))
                {
                    dict[word.ToLower()] = 0;
                }
                dict[word.ToLower()]++;
            }
            var result = new List<string>();
            foreach (var item in dict)
            {
                if (item.Value % 2 != 0)
                {
                    result.Add(item.Key);
                }
            }

            Console.WriteLine(String.Join(" ", result));

        }
    }
}