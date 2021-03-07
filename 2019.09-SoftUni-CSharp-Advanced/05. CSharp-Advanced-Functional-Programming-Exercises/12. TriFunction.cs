using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            Func<string, int, bool> isEqualOrBigger = (name, cl) => name.ToCharArray().Select(x => (int)x).Sum() >= cl;
            Func<string[], int, Func<string, int, bool>, string> firstNameMatch = (arr, num, func) => arr.FirstOrDefault(x => func(x, num));

            int n = int.Parse(Console.ReadLine());
            var names = Console.ReadLine().Split();

            Console.WriteLine(firstNameMatch(names, n, isEqualOrBigger));
        }
    }
}
