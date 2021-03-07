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
            var filters = Console.ReadLine().Split(", ").ToArray();
            var text = Console.ReadLine();

            foreach (var filter in filters)
            {
                text = text.Replace(filter, new string('*', filter.Length));
            }
            Console.WriteLine(text);
        }
    }
}