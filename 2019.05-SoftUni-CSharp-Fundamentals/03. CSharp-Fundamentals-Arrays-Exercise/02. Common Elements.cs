using System;
using System.Linq;
class Program
{
    static void Main()
    {
        string[] firstArray = Console
            .ReadLine()
            .Split(" ");
        string[] secondArray = Console
            .ReadLine()
            .Split(" ");
        string[] resultArray = new string[secondArray.Length];

        for (int j = 0; j < secondArray.Length; j++)
        {
            string element = secondArray[j];
            for (int i = 0; i < firstArray.Length; i++)
            {
                string currentElement = firstArray[i];
                if (element == currentElement)
                {
                    Console.Write(element + " ");
                    break;
                }
            }
        }
    }
}