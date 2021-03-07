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
            var set = new HashSet<string>();
            for (int i = 0; i < n; i++)
            {
                var els = Console.ReadLine().Split();
                foreach (var el in els)
                {
                    set.Add(el);
                }
            }
            var strB = new StringBuilder();
            foreach (var el in set.OrderBy(x => x))
            {
                strB.Append(el + " ");
            }
            Console.WriteLine(strB);
        }
    }
}
