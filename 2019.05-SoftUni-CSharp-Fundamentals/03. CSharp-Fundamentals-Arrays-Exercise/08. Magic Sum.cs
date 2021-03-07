using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int[] nums = Console.ReadLine()
            .Split(" ")
            .Select(int.Parse)
            .ToArray();
        int magicSum = int.Parse(Console.ReadLine());

        for (int i = 0; i < nums.Length; i++)
        {
            int firstNumber = nums[i];
            for (int j = i + 1; j < nums.Length; j++)
            {
                int secondNumber = nums[j];
                if (firstNumber + secondNumber == magicSum)
                    Console.WriteLine(firstNumber + " " + secondNumber);
            }
        }
    }
}