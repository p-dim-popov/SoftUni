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
            Func<int, int, bool> areNotDivBy = (div, x) => x % div != 0;
            var input = Console.ReadLine().Split().Select(int.Parse).Reverse();
            var divisor = int.Parse(Console.ReadLine());
            input.Where(x => areNotDivBy(divisor, x)).ToList().ForEach(x => Console.Write(x + " "));
        }
    }
}
