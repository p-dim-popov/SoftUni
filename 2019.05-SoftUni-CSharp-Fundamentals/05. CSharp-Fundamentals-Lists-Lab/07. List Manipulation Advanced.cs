using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = Console
                .ReadLine()
                .Split(" ")
                .Select(int.Parse)
                .ToList();
            string[] inputCommand = Console
                .ReadLine()
                .Split(" ")
                .ToArray();
            string command = inputCommand[0];
            bool changes = false;
            while (command != "end")
            {
                switch (command)
                {
                    case "Add":
                        int numberToAdd = int.Parse($"{inputCommand[1]}");
                        numbers.Add(numberToAdd);
                        changes = true;
                        break;
                    case "Remove":
                        numbers.Remove(int.Parse($"{inputCommand[1]}"));
                        changes = true;
                        break;
                    case "RemoveAt":
                        numbers.RemoveAt(int.Parse($"{inputCommand[1]}"));
                        changes = true;
                        break;
                    case "Insert":
                        numbers.Insert(int.Parse($"{inputCommand[2]}"), int.Parse($"{inputCommand[1]}"));
                        changes = true;
                        break;
                    case "Contains":
                        if (numbers.Contains(int.Parse(inputCommand[1])))
                            Console.WriteLine("Yes");
                        else
                            Console.WriteLine("No such number");
                        break;
                    case "PrintEven":
                        PrintEven(numbers);
                        Console.WriteLine();
                        break;
                    case "PrintOdd":
                        PrintOdd(numbers);
                        Console.WriteLine();
                        break;
                    case "GetSum":
                        int sum = numbers.Sum();
                        Console.WriteLine(sum);
                        break;
                    case "Filter":
                        Filter(inputCommand[1], int.Parse(inputCommand[2]), numbers);
                        Console.WriteLine();
                        break;
                }

                inputCommand = Console
                .ReadLine()
                .Split(" ")
                .ToArray();
                command = inputCommand[0];
            }
            if (changes)
                Console.WriteLine(String.Join(" ", numbers));
        }

        static void PrintEven(List<int> numbers)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] % 2 == 0)
                    Console.Write(numbers[i] + " ");
            }
        }
        static void PrintOdd(List<int> numbers)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] % 2 != 0)
                    Console.Write(numbers[i] + " ");
            }
        }
        static void Filter(string condition, int number, List<int> numbers)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                switch(condition)
                {
                    case "<":
                        if (numbers[i] < number)
                            Console.Write(numbers[i] + " ");
                        break;
                    case ">":
                        if (numbers[i] > number)
                            Console.Write(numbers[i] + " ");
                        break;
                    case ">=":
                        if (numbers[i] >= number)
                            Console.Write(numbers[i] + " ");
                        break;
                    case "<=":
                        if (numbers[i] <= number)
                            Console.Write(numbers[i] + " ");
                        break;
                }
            }
        }
    }
}