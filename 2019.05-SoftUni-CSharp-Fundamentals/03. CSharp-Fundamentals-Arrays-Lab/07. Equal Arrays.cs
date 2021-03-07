using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int[] array1 = Console
            .ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();

        int[] array2 = Console
            .ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();

        int sum = 0;
        bool notfail = true;
        for (int i = 0; i < array1.Length; i++)
        {
            if (array1[i] != array2[i])
            {
                Console.WriteLine($"Arrays are not identical. Found difference at {i} index");
                notfail = false;
                break;

            }
            else
                sum += array1[i];
        }
        if (notfail)
        Console.WriteLine($"Arrays are identical. Sum: {sum}");
    }
}