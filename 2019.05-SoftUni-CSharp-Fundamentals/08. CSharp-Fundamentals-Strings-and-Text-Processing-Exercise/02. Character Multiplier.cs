using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    class Program
    {
        static void Main()
        {
            string[] strings = Console.ReadLine()
                .Split(" ");

            string firstStr = strings[0];
            string secondStr = strings[1];
            string shorter, bigger;

            if (firstStr.Length > secondStr.Length)
            {
                shorter = secondStr;
                bigger = firstStr;
            }
            else
            {
                shorter = firstStr;
                bigger = secondStr;
            }
            int totalSum = 0;

            for (int i = 0; i < shorter.Length; i++)
            {
                totalSum += shorter[i] * bigger[i];
            }
            if (bigger.Length > shorter.Length)
            {
                totalSum += bigger
                    .TakeLast(bigger.Length - shorter.Length)
                    .Select(x => (int)x)
                    .Sum();
            }

            Console.WriteLine(totalSum);
        }
    }
}