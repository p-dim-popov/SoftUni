using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            var text = Console.ReadLine();
            var dict = new SortedDictionary<char, int>();
            for (int i = 0; i < text.Length; i++)
            {
                if (!dict.ContainsKey(text[i]))
                {
                    dict[text[i]] = 0;
                }
                dict[text[i]]++;
            }
            foreach (var kvp in dict)
            {
                Console.WriteLine(kvp.Key + ": " + kvp.Value + " time/s");
            }
        }
    }
}
