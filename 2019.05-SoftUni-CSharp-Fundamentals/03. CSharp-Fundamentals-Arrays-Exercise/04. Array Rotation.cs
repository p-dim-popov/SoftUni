using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int[] numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            int firstNumber = numbers[0];
            for (int j = 0; j < numbers.Length - 1; j++)
            {
                numbers[j] = numbers[j + 1];
            }
            numbers[numbers.Length - 1] = firstNumber;
        }

        Console.Write(String.Join(" ", numbers));
    }
}