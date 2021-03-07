using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    class Program
    {
        static void Main()
        {
            var dict = new Dictionary<string, int>();

            string input = Console.ReadLine();
            string item = String.Empty;

            for (int line = 1; ; line++)
            {
                if (input == "stop")
                {
                    break;
                }

                if (line % 2 != 0) //odd line
                {
                    item = input;
                }
                else if (line % 2 == 0) //even line
                {
                    if (!dict.ContainsKey(item))
                    {
                        dict[item] = 0;
                    }
                    dict[item] += int.Parse(input);
                }

                input = Console.ReadLine();
            }

            foreach (var kvp in dict)
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
            }
        }
    }
}