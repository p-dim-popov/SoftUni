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
            var stack = new Stack<string>(parts.Reverse());

            while (stack.Count > 1)
            {
                var el = stack.Pop();
                if(el.Equals("+") || el.Equals("-"))
                {
                    var numb = int.Parse(stack.Pop());
                    result += int.Parse(el + numb);
                }
                else
                {
                    result += int.Parse(el);
                }
            }
            Console.WriteLine(result);
        }
    }
}
