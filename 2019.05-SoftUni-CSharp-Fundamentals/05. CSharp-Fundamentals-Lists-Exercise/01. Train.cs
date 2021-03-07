using System;
using System.Linq;
using System.Collections.Generic;

namespace testSoftuni
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> wagons = Console
                .ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();
            int maxPass = int.Parse(Console.ReadLine());
            string[] input = Console
                .ReadLine()
                .Split()
                .ToArray();
            while(input[0] != "end")
            {
                if (input[0] == "Add")
                {
                    wagons.Add(int.Parse(input[1]));
                }
                else
                {
                    for (int i = 0; i < wagons.Count; i++)
                    {
                        if (wagons[i] + int.Parse(input[0]) <= maxPass)
                        {
                            wagons[i] += int.Parse(input[0]);
                            break;
                        }
                    }
                }
                input = Console
                .ReadLine()
                .Split()
                .ToArray();
            }
            Console.WriteLine(String.Join(" ", wagons));

        }
    }
}
