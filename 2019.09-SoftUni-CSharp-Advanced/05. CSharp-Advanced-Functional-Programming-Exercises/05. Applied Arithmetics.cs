using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            Func<int, int> addFunc = num => num += 1;
            Func<int, int> multiplyFunc = num => num *= 2;
            Func<int, int> subtractFunc = num => num -= 1;
            Action<int[]> printNums = nums => Console.WriteLine(string.Join(' ', nums));

            int[] numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            string command = Console.ReadLine();
            while (command != "end")
            {
                if (command == "add")
                {
                    numbers = numbers.Select(addFunc).ToArray();
                }
                else if (command == "multiply")
                {
                    numbers = numbers.Select(multiplyFunc).ToArray();
                }
                else if (command == "subtract")
                {
                    numbers = numbers.Select(subtractFunc).ToArray();
                }
                else if (command == "print")
                {
                    printNums(numbers);
                }
                command = Console.ReadLine();
            }
        }
    }
}
