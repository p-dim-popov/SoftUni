using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            //1 10
            //odds
            Predicate<int> isEvenPredicate = num => num % 2 == 0;
            Action<List<int>> printNumbers = nums => Console.WriteLine(string.Join(' ', nums));
            var input =Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            List<int> numbers = new List<int>();

            int startNumber = input[0];
            int endNumber = input[1];

            for (int i = startNumber; i <= endNumber; i++)
            {
                numbers.Add(i);
            }

            string numberType = Console.ReadLine();

            if (numberType == "even")
            {
                numbers.RemoveAll(x => !isEvenPredicate(x));
            }
            else
            {
                numbers.RemoveAll(x =>isEvenPredicate(x));
            }
            printNumbers(numbers);
        }
    }
}