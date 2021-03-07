using System;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            var set = new System.Collections.Generic.HashSet<string>();
            for (int i = 0; i < n; i++)
            {
                set.Add(Console.ReadLine());
            }
            foreach (var item in set)
            {
                Console.WriteLine(item);
            }
        }
    }
}
