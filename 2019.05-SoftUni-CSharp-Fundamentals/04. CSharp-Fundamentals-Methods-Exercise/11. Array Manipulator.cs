using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int[] initArray = Console
            .ReadLine()
            .Split(" ")
            .Select(int.Parse)
            .ToArray();
        string inputCommands = String.Empty;
        while (inputCommands != "end")
        {

        }
    }
    static void Exchange(int index, int[] initArray)
    {
        if (index > initArray.Length)
        {
            Console.WriteLine("Invalid index");
            return;
        }
        int[] exchangedArray = new int[initArray.Length];
        //int[] secondArray = new int[initArrayLength - 1 - index];
        for (int i = 0; i < initArray.Length - 1 - index; i++)
        {
            exchangedArray[1 + index + i] = initArray[i];
        }
        for (int i = 0; i < 1 + index; i++)
        {
            exchangedArray[i] = initArray[1 + index];
        }
        Console.WriteLine();
        return;
    }
}