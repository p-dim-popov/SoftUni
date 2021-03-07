using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            var input = Console.ReadLine().ToCharArray();
            var stack = new Stack<char>();
            bool error = false;

            foreach (var @char in input)
            {
                switch (@char)
                {
                    case '(':
                        stack.Push(')');
                        break;
                    case '[':
                        stack.Push(']');
                        break;
                    case '{':
                        stack.Push('}');
                        break;
                    case ')':
                    case ']':
                    case '}':
                        if (!stack.Any() || stack.Pop() != @char)
                        {
                            error = true;
                        }
                        break;
                    default:
                        break;
                }

                if (error)
                {
                    Console.WriteLine("NO");
                    return;
                }
            }
            Console.WriteLine("YES");
        }
    }
}
