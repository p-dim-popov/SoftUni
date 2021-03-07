using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            Action<string> print = item => Console.WriteLine("Sir " + item);

            Console.ReadLine()
                .Split()
                .ToList()
                .ForEach(print);
        }
    }
}
