using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    class Program
    {
        static void Main()
        {
            string line;
            while (( line =Console.ReadLine()) != "end")
            {
                var result = line.Reverse().ToArray();
                Console.WriteLine(line + " = " + string.Join("",result));
            }
        }
    }
}