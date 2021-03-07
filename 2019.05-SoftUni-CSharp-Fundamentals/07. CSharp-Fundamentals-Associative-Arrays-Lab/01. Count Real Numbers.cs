using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    class Program
    {
        static void Main()
        {
            var integers = Console.ReadLine().Split().ToList();
            var dict = new SortedDictionary<string, int>();
            foreach (var number in integers)
            {
                if (!dict.ContainsKey(number))
                {
                    dict[number] = 0;
                }

                dict[number]++;
            }

            foreach(var item in dict)
            {
                Console.WriteLine($"{item.Key} -> {item.Value}");
            }
        }
    }
}