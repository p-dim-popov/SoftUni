using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            Predicate<string> lessThan = x => x.Length <= n;
            Action<string> print = x => Console.WriteLine(x);

            Console.ReadLine().Split().ToList().FindAll(lessThan).ToList().ForEach(print);
            
        }
    }
}
