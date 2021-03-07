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
            List<float> numbers = Console
                .ReadLine()
                .Split(" ")
                .Select(float.Parse)
                .ToList();
            if (numbers.Count < 2)
            {
                Console.WriteLine(numbers[0]);
                return;
            }
            else
            {
                float currentNumber = numbers[1];
                float previousNumber = numbers[0];
                for (int i = 1; i < numbers.Count; i++)
                {
                    currentNumber = numbers[i];
                    previousNumber = numbers[i - 1];

                    if (currentNumber == previousNumber)
                    {
                        numbers.RemoveAt(i - 1);
                        numbers[i - 1] *= 2;
                        i = 0;
                        if (numbers.Count < 2)
                        {
                            Console.WriteLine(numbers[0]);
                            return;
                        }
                    }
                }
                Console.WriteLine(String.Join(" ", numbers));
            }
        }
    }
}