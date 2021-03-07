using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Program
{
    class Program
    {
        static void Main()
        {
            string text = Console.ReadLine();
            var regex = new Regex(@"(?<=\s)([a-z]+|\d+)(\d+|\w+|\.+|-+)([a-z]+|\d+)\@[a-z]+\-?[a-z]+\.[a-z]+(\.[a-z]+)?");
            var emails = regex.Matches(text);
            foreach (Match email in emails)
            {
                Console.WriteLine(email.Value);
            }

        }
    }
}