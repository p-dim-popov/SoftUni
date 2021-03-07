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
                .Where(x => x.Length % 2 == 0)
                .ToList();
            Console.WriteLine(String.Join(Environment.NewLine, input));
        }
    }
}