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
            var n = int.Parse(Console.ReadLine());
            var dict = new SortedDictionary<int, int>();
            for (int i = 0; i < n; i++)
            { 
                var number = int.Parse(Console.ReadLine());
                if (!dict.ContainsKey(number))
                {
                    dict[number] = 0;
                }
                dict[number]++;
            }
            foreach (var kvp in dict)
            {
                if(kvp.Value % 2 == 0)
                {
                    Console.WriteLine(kvp.Key);
                    return;
                }
            }
        }
    }
}
