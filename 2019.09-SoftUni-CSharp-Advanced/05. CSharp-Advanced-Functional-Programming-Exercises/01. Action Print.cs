using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            Action<string[]> print = items => Console.WriteLine(String.Join(Environment.NewLine, items));
            string[] names = Console.ReadLine()
                .Split();
            print(names);
        }
    }
}
