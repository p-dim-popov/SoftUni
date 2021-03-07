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
        int currentCount = 1;
        int maxCount = 1;
        int number = nums[0];

        for (int i = 1; i < nums.Length; i++)
        {
            int previousNum = nums[i - 1];
            int currentNum = nums[i];

            if (previousNum == currentNum)
            {
                currentCount++;

                if (currentCount > maxCount)
                {
                    maxCount = currentCount;
                    number = currentNum;
                }
            }
            else
            {
                currentCount = 1;
            }
        }
        for (int j = 0; j < maxCount; j++)
        {
            Console.Write(number + " ");
        }
    }
}