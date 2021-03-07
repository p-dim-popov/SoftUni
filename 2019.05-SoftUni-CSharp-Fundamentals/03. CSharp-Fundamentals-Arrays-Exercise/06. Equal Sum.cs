using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int[] nums = Console.ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();
        int leftSum = 0;
        int rightSum = nums.Sum();

        for (int i = 0; i < nums.Length; i++)
        {
            rightSum -= nums[i];

            if (leftSum == rightSum)
            {
                Console.WriteLine(i);
                return;
            }
            leftSum += nums[i];
        }

        Console.WriteLine("no");

    }
}