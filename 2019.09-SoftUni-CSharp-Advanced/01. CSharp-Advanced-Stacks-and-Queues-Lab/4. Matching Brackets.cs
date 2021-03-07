using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            var stack = new Stack<int>();
            for (int i = 0; i < input.Length; i++)
            {
                var ch = input[i];
                if(ch == '(')
                {
                    stack.Push(i);
                }
                else if(ch == ')')
                {
                    var leftIndex = stack.Pop();
                    var exp = input.Substring(leftIndex, i - leftIndex + 1);
                    Console.WriteLine(exp);
                }
            } 
        }
    }
}
