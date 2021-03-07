using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    class Program
    {
        static void Main()
        {
            var input = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray()
                .OrderByDescending(x => x)
                .ToArray()
                .Take(3)
                .ToArray();
            Console.WriteLine(string.Join(" ", input));
        }
    }
}