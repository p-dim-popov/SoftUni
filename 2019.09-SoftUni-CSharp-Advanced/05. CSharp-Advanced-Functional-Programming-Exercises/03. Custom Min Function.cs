using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            Func<int[], int> minFunc = inputNumbers =>
            {
                int minValue = int.MaxValue;
                foreach (var currentNumber in inputNumbers)
                {
                    if (currentNumber < minValue)
                    {
                        minValue = currentNumber;
                    }
                }
                return minValue;
            };

            int[] numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            int result = minFunc(numbers);
            Console.WriteLine(result);
        }
    }
}
