using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var stack = new Stack<int>();
            while (n-- > 0)
            {
                var query = Console.ReadLine().Split();
                var type = query[0];
                if (type == "1")
                {
                    var x = int.Parse(query[1]);
                    stack.Push(x);
                }
                else if (stack.Count > 0)
                {
                    if (type == "2")
                        stack.Pop();
                    else if (type == "3")
                        Console.WriteLine(stack.Max());
                    else if (type == "4")
                        Console.WriteLine(stack.Min());
                }
            }
            Console.WriteLine(string.Join(", ", stack));
        }
    }
}