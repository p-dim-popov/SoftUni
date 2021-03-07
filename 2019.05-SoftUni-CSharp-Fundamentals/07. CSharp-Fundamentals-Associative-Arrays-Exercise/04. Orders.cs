using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSoftUni
{
    class Program
    {
        static void Main(string[] args)
        {
            var products = new Dictionary<string, ProductInfo>();

            string line = Console.ReadLine();
            while (line != "buy")
            {
                var tokens = line
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();
                string name = tokens[0];
                double price = double.Parse(tokens[1]);
                int quantity = int.Parse(tokens[2]);

                if (!products.ContainsKey(name))
                {
                    products[name] = new ProductInfo(price, quantity);
                }
                else
                {
                    products[name].Quantity += quantity;
                    products[name].Price = price;
                }
                line = Console.ReadLine();
            }
            foreach(var kvp in products)
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
            }
        }
    }

    class ProductInfo
    {
        public ProductInfo(double price, int quantity)
        {
            this.Quantity = quantity;
            this.Price = price;
        }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public override string ToString()
        {
            return $"{this.Price*this.Quantity:f2}";
        }
    }
}
