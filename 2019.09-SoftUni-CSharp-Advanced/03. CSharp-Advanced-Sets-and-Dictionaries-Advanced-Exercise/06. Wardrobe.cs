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
            var dict = new Dictionary<string, Dictionary<string, int>>();
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split(" -> ");
                var color = input[0];
                var clothes = input[1].Split(',');
                if (!dict.ContainsKey(color))
                {
                    dict[color] = new Dictionary<string, int>();
                }
                
                foreach (var cloth in clothes)
                {
                    if (!dict[color].ContainsKey(cloth))
                    {
                        dict[color][cloth] = 0;
                    }
                    dict[color][cloth]++;
                }
            }

            var searchFor = Console.ReadLine().Split();
            var colorSearch = searchFor[0];
            var clothSearch = searchFor[1];


            var strB = new StringBuilder();
            foreach (var kvp in dict)
            {
                strB.Append(kvp.Key + " clothes:" + Environment.NewLine);
                foreach (var item in kvp.Value)
                {
                    strB.Append("* " + item.Key + " - " + item.Value);
                    if (kvp.Key == colorSearch && item.Key == clothSearch)
                    {
                        strB.Append(" (found!)");
                    }
                    strB.Append(Environment.NewLine);
                }
            }
            Console.WriteLine(strB);
        }
    }
}
