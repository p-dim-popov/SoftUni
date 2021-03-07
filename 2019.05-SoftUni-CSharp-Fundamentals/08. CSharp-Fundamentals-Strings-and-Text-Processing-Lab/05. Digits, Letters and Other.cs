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
            string str = Console.ReadLine();
            var result = new StringBuilder();
            foreach(char ch in str)
            {
                if(char.IsDigit(ch))
                {
                    result.Append(ch);
                }
            }
            result.Append($"{Environment.NewLine}");
            foreach (char ch in str)
            {
                if (char.IsLetter(ch))
                {
                    result.Append(ch);
                }
            }
            result.Append($"{Environment.NewLine}");
            foreach (char ch in str)
            {
                if (!char.IsLetterOrDigit(ch))
                {
                    result.Append(ch);
                }
            }
            Console.WriteLine(result);
        }
    }
}