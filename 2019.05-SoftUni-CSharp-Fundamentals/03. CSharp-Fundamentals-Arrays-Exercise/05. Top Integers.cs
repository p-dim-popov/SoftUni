using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int[] numbersArray = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int biggestNumber = numbersArray[0];
        int[] topInts = new int[numbersArray.Length];
        int a = 0;
        for (int i = 0; i < numbersArray.Length - 1; i++)
        {
            int currentNumber = numbersArray[i];
            bool isTopInt = true;
            for (int j = i + 1; j < numbersArray.Length; j++)
            {
                int otherNumber = numbersArray[j];
                if (currentNumber <= otherNumber)
                {
                    isTopInt = false;
                    break;
                }
            }
            if (isTopInt)
                Console.Write(currentNumber + " ");
        }
        Console.Write(numbersArray[numbersArray.Length - 1]);
    }
}