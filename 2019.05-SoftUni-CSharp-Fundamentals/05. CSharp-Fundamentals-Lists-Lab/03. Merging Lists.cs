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
            List<int> numbers = Console
                .ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToList();
            List<int> numbers2 = Console
                            .ReadLine()
                            .Split(" ")
                            .Select(int.Parse)
                            .ToList();
            List<int> result = new List<int>();
            int count = numbers.Count + numbers2.Count - 1;

            for (int i = 0; i < count; i++)
            {
                if (numbers.Count > 0)
                {
                    result.Add(numbers[0]);
                    numbers.Reverse();
                    numbers.RemoveAt(numbers.Count - 1);
                    numbers.Reverse();
                }
                if (numbers2.Count > 0)
                {
                    result.Add(numbers2[0]);
                    numbers2.Reverse();
                    numbers2.RemoveAt(numbers2.Count - 1);
                    numbers2.Reverse();
                }
            }
            Console.WriteLine(String.Join(" ", result));
        }
    }
}