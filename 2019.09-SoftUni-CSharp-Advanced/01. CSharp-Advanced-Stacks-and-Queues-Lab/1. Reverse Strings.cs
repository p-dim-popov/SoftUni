using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var stack = new System.Collections.Generic.Stack<char>();
            foreach(var ch in input)
            {
                stack.Push(ch);
            }
            while(stack.Count != 0)
            {
                Console.Write(stack.Pop());
            }
        }
    }
}
