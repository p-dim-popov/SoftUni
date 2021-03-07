using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    class Program
    {
        static void Main()
        {
            string someText = Console.ReadLine();

            var lettersCount = new Dictionary<char, int>();

            for (int i = 0; i < someText.Length; i++)
            {
                char letter = someText[i];

                if (letter != ' ')
                {
                    if (!lettersCount.ContainsKey(letter))
                    {
                        lettersCount[letter] = 0;
                    }
                    lettersCount[letter]++;
                }
            }

            foreach (var kvp in lettersCount)
            {
                char key = kvp.Key;
                int value = kvp.Value;

                Console.WriteLine($"{key} -> {value}");
            }
        }
    }
}