using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Numerics;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            int input = int.Parse(Console.ReadLine());
            BigInteger factorial = 1;
            for (int i =  1; i < input + 1; i++)
            {
                factorial *= i;
            }
            Console.WriteLine(factorial);
        }
    }
}