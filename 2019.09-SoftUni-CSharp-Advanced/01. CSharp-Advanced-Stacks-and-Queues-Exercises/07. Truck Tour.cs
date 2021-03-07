using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            int[] pumps = new int[n];

            for (int i = 0; i < n; i++)
            {
                int[] integers = Console.ReadLine().Split().Select(int.Parse).ToArray();
                pumps[i] = integers[0] - integers[1];
            }

            int current = 0;
            int position = 0;

            for (int i = 0; i < n; i++)
            {
                current += pumps[i];

                if (current < 0)
                {
                    current = 0;
                    position = i + 1;
                }
            }
            Console.WriteLine(position);
        }
    }
}
