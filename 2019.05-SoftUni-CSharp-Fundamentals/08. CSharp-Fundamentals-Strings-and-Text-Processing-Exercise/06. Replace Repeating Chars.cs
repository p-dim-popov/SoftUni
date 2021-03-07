using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    class Program
    {
        static void Main()
        {
            string word = Console.ReadLine();
            var builder = new StringBuilder();
            char previousChar = word[0];
            builder.Append(previousChar);
            foreach(var ch in word)
            {
                if(ch == previousChar)
                {
                    continue;
                }
                builder.Append(ch);
                previousChar = ch;
            }
            Console.WriteLine(builder);
        }
    }
}