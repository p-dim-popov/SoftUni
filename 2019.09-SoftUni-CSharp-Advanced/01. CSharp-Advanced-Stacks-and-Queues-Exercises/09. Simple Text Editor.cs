using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            int commandCount = int.Parse(Console.ReadLine());
            var versions = new Stack<string>();
            var text = new System.Text.StringBuilder();

            for (int i = 0; i < commandCount; i++)
            {
                var commandProps = Console.ReadLine().Split();
                string command = commandProps[0];

                switch (command)
                {
                    case "1":
                        versions.Push(text.ToString());
                        string textToAdd = commandProps[1];
                        text.Append(textToAdd);
                        break;
                    case "2":
                        versions.Push(text.ToString());
                        int removeElementsCount = int.Parse(commandProps[1]);
                        text.Remove(text.Length - removeElementsCount, removeElementsCount);
                        break;
                    case "3":
                        int index = int.Parse(commandProps[1]) - 1;
                        Console.WriteLine(text[index]);
                        break;
                    case "4":
                        text.Clear();
                        text.Append(versions.Pop());
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
