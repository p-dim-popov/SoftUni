using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var exp = Console.ReadLine();
            var parts = exp.Split(' ');
            var result = 0;
            var stack = new Stack<string>(parts);
            var input = Console.ReadLine();
            while (input.ToLower() != "end")
            {
                var splInput = input.Split();
                var command = splInput[0].ToLower();
                switch (command)
                {
                    case "add":
                        for (int i = 1; i < splInput.Length; i++)
                        {
                            stack.Push(splInput[i]);
                        }
                        break;
                    case "remove":
                        var lengthToRemove = int.Parse(splInput[1]);
                        if (stack.Count >= lengthToRemove)
                        {
                            for (int i = 0; i < lengthToRemove; i++)
                            {
                                stack.Pop();
                            }
                        }
                        break;
                    default:
                        break;
                }
                input = Console.ReadLine();
            }
            while (stack.Count > 0)
            {
                result += int.Parse(stack.Pop());
            }
            Console.WriteLine("Sum: " + result);
        }
    }
}
