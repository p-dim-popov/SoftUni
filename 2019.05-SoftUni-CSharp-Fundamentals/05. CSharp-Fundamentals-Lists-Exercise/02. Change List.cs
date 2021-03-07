using System;
using System.Linq;
using System.Collections.Generic;

namespace testSoftuni
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> listToManipulate = Console
                .ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();
            string[] command = Console
                .ReadLine()
                .Split()
                .ToArray();
            while (command[0] != "end")
            {
                int element = int.Parse(command[1]);
                if (command[0] == "Delete")
                {
                    while (listToManipulate.Contains(element))
                    {
                        listToManipulate.Remove(int.Parse(command[1]));
                    }
                }
                else if (command[0] == "Insert")
                {
                    int position = int.Parse(command[2]);
                    listToManipulate.Insert(position, element);
                }
                command = Console
                .ReadLine()
                .Split()
                .ToArray();
            }
            Console.WriteLine(String.Join(" ", listToManipulate));
        }
    }
}
