using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console
                .ReadLine()
                .Split()
                .ToList();
            for (int i = 0; i < input.Count; i++)
            {
                var rnd = new Random();
                var randomIndex = rnd.Next(0, input.Count);

                var currentElement = input[i];
                var randomElement = input[randomIndex];

                input[i] = randomElement;
                input[randomIndex] = currentElement;

            }
            foreach (var word in input)
            {
                Console.WriteLine(word);
            }
        }
    }
}