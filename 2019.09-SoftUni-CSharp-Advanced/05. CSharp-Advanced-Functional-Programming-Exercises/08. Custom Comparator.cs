using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            Func<int, int, int> sortFunc = (a, b) => a.CompareTo(b);
            Action<int[], int[]> print = (x, y) => Console.WriteLine(string.Join(' ', x) + " " + string.Join(' ', y));

            var evenNumbers = numbers.Where(x => x % 2 == 0).ToArray();
            var oddNumbers = numbers.Where(x => x % 2 != 0).ToArray();

            Array.Sort(evenNumbers, new Comparison<int>(sortFunc));
            Array.Sort(oddNumbers, new Comparison<int>(sortFunc));
            print(evenNumbers, oddNumbers);
        }
    }
}
