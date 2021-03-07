using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            var args = Console.ReadLine().Split();
            int n = int.Parse(args[0]);
            int s = int.Parse(args[1]);
            int x = int.Parse(args[2]);
            var queue = new Queue<int>(n);
            Console.ReadLine().Split()
                .Select(int.Parse)
                .ToList()
                .ForEach(item => queue.Enqueue(item));

            for (int i = 0; i < s; i++)
            {
                if (queue.Count > 0)
                    queue.Dequeue();
            }
            if (queue.Contains(x))
            {
                Console.WriteLine(true.ToString().ToLower());
            }
            else if (!queue.Contains(x) && queue.Count > 0)
            {
                var smallest = int.MaxValue;
                foreach (var item in queue)
                {
                    smallest = item < smallest ? item : smallest;
                }
                Console.WriteLine(smallest);
            }
            else
            {
                Console.WriteLine(0);
            }
        }
    }
}
