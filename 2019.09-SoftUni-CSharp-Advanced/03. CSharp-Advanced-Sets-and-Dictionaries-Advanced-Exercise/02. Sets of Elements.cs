using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            var nm = Console.ReadLine().Split();
            int n = int.Parse(nm[0]);
            int m = int.Parse(nm[1]);

            var set1 = new HashSet<int>();
            var set2 = new HashSet<int>();
            for (int i = 0; i < n; i++)
            {
                set1.Add(int.Parse(Console.ReadLine()));
            }
            for (int i = 0; i < m; i++)
            {
                set2.Add(int.Parse(Console.ReadLine()));
            }
            var dict = new Dictionary<int, int>();
            foreach (var item in set1)
            {
                if(!dict.ContainsKey(item))
                {
                    dict[item] = 0;
                }
                dict[item]++;
            }
            foreach (var item in set2)
            {
                if (!dict.ContainsKey(item))
                {
                    dict[item] = 0;
                }
                dict[item]++;
            }
            var res = new System.Text.StringBuilder();
            foreach(var kvp in dict.Where(x => x.Value == 2))
            {
                res.Append(kvp.Key + " ");
            }
            Console.WriteLine(res);
        }
    }
}
