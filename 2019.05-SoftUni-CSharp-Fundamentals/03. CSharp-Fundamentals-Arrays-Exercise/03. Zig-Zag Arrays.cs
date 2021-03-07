using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int[] firstArray = new int[n];
        int[] secondArray = new int[n];

        for (int i = 0; i < n; i++)
        {
            int[] numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            if (i%2 == 0)
            {
                firstArray[i] = numbers[0];
                secondArray[i] = numbers[1];
            }
            else
            {
                firstArray[i] = numbers[1];
                secondArray[i] = numbers[0];
            }
        }
        Console.WriteLine(String.Join(" ", firstArray));
        Console.WriteLine(String.Join(" ", secondArray));

    }
}