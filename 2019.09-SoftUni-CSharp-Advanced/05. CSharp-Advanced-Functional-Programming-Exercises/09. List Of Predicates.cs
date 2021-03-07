using System;
using System.Collections.Generic;
using System.Linq;

class Program
{

    static void Main()
    {
        Func<int, int[], List<int>> getNumbers = (num, divisors) =>
        {
            List<int> res = new List<int>();
            for (int i = 1; i <= num; i++)
            {
                bool isValid = true;
                foreach (var d in divisors)
                {
                    Predicate<int> uncleanCut = x => i % x != 0;
                    if (uncleanCut(d))
                    {
                        isValid = false;
                        break;
                    }
                }
                if (isValid)
                {
                    res.Add(i);
                }
            }
            return res;
        };

        int number = int.Parse(Console.ReadLine());
        int[] numbers = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        var result = getNumbers(number, numbers);
        Console.WriteLine(string.Join(" ", result));
    }
}