using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> number = Console
                .ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToList();
            for(int i = 0; i < number.Count; i++)
            {
                if (number[i] < 0)
                {
                    number.RemoveAt(i);
                    i--;
                }
            }
            if (number.Count == 0)
            {
                Console.WriteLine("empty");
                return;
            }
            number.Reverse();
            Console.WriteLine(String.Join(" ", number));
        }
    }
}