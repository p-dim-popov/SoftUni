using System;
using System.Linq;
using System.Collections.Generic;

namespace testSoftuni
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            
            List<string> namesOnTheList = new List<string>();
            for (int i = 0; i < n; i++)
            {
                string[] input = Console
                .ReadLine()
                .Split()
                .ToArray();

                string name = input[0];
                string goingOrNot = input[2];
                if (goingOrNot == "going!")
                {
                    if (namesOnTheList.Contains(name))
                    {
                        Console.WriteLine(name + " is already in the list!");
                    }
                    else
                    {
                        namesOnTheList.Add(name);
                    }
                }
                else if (goingOrNot == "not")
                {
                    if (namesOnTheList.Contains(name))
                    {
                        namesOnTheList.Remove(name);
                    }
                    else
                    {
                        Console.WriteLine(name + " is not in the list!");
                    }
                }
            }
            Console.WriteLine(String.Join("\n", namesOnTheList));
        }
    }
}
