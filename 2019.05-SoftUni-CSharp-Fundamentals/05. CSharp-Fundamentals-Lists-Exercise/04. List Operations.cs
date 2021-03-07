using System;
using System.Linq;
using System.Collections.Generic;

namespace testSoftuni
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = Console
                .ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();
            string[] command = Console.ReadLine().Split().ToArray();
            while (command[0] != "End")
            {
                switch (command[0])
                {
                    case "Add":
                        numbers.Add((int.Parse(command[1])));
                        break;
                    case "Insert":
                        if (int.Parse(command[2]) > numbers.Count - 1 || int.Parse(command[2]) < 0)
                        {
                            Console.WriteLine("Invalid index");
                        }
                        else
                            numbers.Insert(int.Parse(command[2]), int.Parse(command[1]));
                        break;
                    case "Remove":
                        if (int.Parse(command[1]) > numbers.Count - 1 || int.Parse(command[1]) < 0)
                        {
                            Console.WriteLine("Invalid index");
                        }
                        else
                            numbers.RemoveAt(int.Parse(command[1]));
                        break;
                    case "Shift":
                        if (command[1] == "left")
                        {
                            for (int i = 0; i < int.Parse(command[2]) % numbers.Count; i++)
                            {
                                numbers.Add(numbers[0]);
                                numbers.Reverse();
                                numbers.RemoveAt(numbers.Count - 1);
                                numbers.Reverse();
                            }
                        }
                        else if (command[1] == "right")
                        {
                            for (int i = 0; i < int.Parse(command[2]) % numbers.Count; i++)
                            {
                                numbers.Reverse();
                                numbers.Add(numbers[0]);
                                numbers.Reverse();
                                numbers.RemoveAt(numbers.Count - 1);

                            }
                        }
                        break;
                }
                command = Console.ReadLine().Split().ToArray();
            }
            Console.WriteLine(String.Join(" ", numbers));
        }
    }
}
