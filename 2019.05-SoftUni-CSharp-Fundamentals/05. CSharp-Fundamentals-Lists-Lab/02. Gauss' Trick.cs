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
            List<long> numbers = Console
                .ReadLine()
                .Split(" ")
                .Select(long.Parse)
                .ToList();
            if (numbers.Count == 1 /*&& numbers.Count == 1*/)
            {
                Console.WriteLine(numbers[0]);
                return;
            }
            for(int i = 0; i < (numbers.Count + 1)/2; i++)
            {
                numbers[i] += numbers[numbers.Count - 1];
                numbers.RemoveAt(numbers.Count - 1);
            }
            Console.WriteLine(String.Join(" ", numbers));
            
        }
    }
}