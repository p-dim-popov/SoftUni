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
            while (command != "end")
            {
                switch(command)
                {
                    case "Add":
                        int numberToAdd = int.Parse($"{inputCommand[1]}");
                        numbers.Add(numberToAdd);
                        break;
                    case "Remove":
                        numbers.Remove(int.Parse($"{inputCommand[1]}"));
                        break;
                    case "RemoveAt":
                        numbers.RemoveAt(int.Parse($"{inputCommand[1]}"));
                        break;
                    case "Insert":
                        numbers.Insert(int.Parse($"{inputCommand[2]}"), int.Parse($"{inputCommand[1]}"));
                        break;
                }

                inputCommand = Console
                .ReadLine()
                .Split(" ")
                .ToArray();
                command = inputCommand[0];
            }
            Console.WriteLine(String.Join(" ", numbers));
        }
    }
}