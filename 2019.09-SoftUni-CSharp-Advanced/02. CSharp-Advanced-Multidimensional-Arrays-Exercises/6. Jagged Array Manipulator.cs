using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            byte n = byte.Parse(Console.ReadLine());
            var jaggedArray = new decimal[n][];
            for (int i = 0; i < n; i++)
            {
                jaggedArray[i] = Console.ReadLine().Split().Select(decimal.Parse).ToArray();
            }
            for (int i = 0; i < jaggedArray.Length - 1; i++)
            {
                if(jaggedArray[i].Length == jaggedArray[i + 1].Length)
                {
                    for (int j = 0; j < jaggedArray[i].Length; j++)
                    {
                        jaggedArray[i][j] *= 2;
                        jaggedArray[i + 1][j] *= 2;
                    }
                }
                else
                {
                    for (int j = 0; j < jaggedArray[i].Length; j++)
                    {
                        jaggedArray[i][j] /= 2;
                    }
                    for (int j = 0; j < jaggedArray[i + 1].Length; j++)
                    {
                        jaggedArray[i + 1][j] /= 2;
                    }
                }
            }
            while (true)
            {
                var input = Console.ReadLine().Split();
                var command = input[0];
                if (command == "End") break;
                var row = int.Parse(input[1]);
                var col = int.Parse(input[2]);
                if (row < 0 || row >= jaggedArray.Length ||
                        col < 0 || col >= jaggedArray[row].Length)
                {
                    continue;
                }
                var value = int.Parse(input[3]);
                if (command == "Add")
                {
                    jaggedArray[row][col] += value;
                }
                else if (command == "Subtract")
                {
                    jaggedArray[row][col] -= value;
                }
            }
            foreach (var item in jaggedArray)
            {
                Console.WriteLine(string.Join(' ', item));
            }
        }
    }
}