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
            string numberAsString = Console.ReadLine().TrimStart('0');
            int multiplier = int.Parse(Console.ReadLine());
            var builder = new StringBuilder();
            int onMind = 0;
            for(int i = numberAsString.Length - 1; i + 1 > 0; i--)
            {
                int digit = int.Parse(numberAsString[i].ToString());
                int result = digit * multiplier + onMind;
                
                builder.Append(result % 10);
                onMind = result / 10;
            }
            if(onMind != 0)
            {
                builder.Append(onMind);
            }
            string resultNumber = String.Join("", builder.ToString().Reverse()).TrimStart('0');
            if(resultNumber == String.Empty)
            {
                Console.WriteLine("0");
                return;
            }
            Console.WriteLine($"{resultNumber}");
        }
    }
}