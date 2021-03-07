using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<string> listOfProducts = new List<string>();
            
            for (int i = 0; i < n; i++)
            {
                listOfProducts.Add(Console.ReadLine());
            }
            listOfProducts.Sort();
            for (int i = 1; i < n + 1; i++)
            {
                Console.WriteLine($"{i}.{listOfProducts[i - 1]}");
            }
            
        }
    }
}