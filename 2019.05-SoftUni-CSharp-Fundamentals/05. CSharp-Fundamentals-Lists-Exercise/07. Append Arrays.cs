using System;
using System.Linq;
using System.Collections.Generic;

namespace testSoftuni
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> inputBad = Console
                            .ReadLine()
                            .Split("|")
                            .ToList();
            inputBad.Reverse();
            List<int> output = String.Join(" ", inputBad)
                            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToList();
            Console.WriteLine(String.Join(" ", output));
        }
    }
}
