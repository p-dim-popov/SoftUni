using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Numerics;

namespace Program
{
    class Program
    {
        static void Main()
        {
            var boxes = new List<Box>();
            string line = Console.ReadLine();

            while (line != "end")
            {
                string[] input = line.Split().ToArray();

                string serialNumber = input[0];
                string itemName = input[1];
                int itemQuantity = int.Parse(input[2]);
                double itemPrice = double.Parse(input[3]);

                boxes.Add(new Box
                {
                    SerialNumber = serialNumber,
                    Item = new Item()
                    {
                        Name = itemName,
                        Price = itemPrice
                    },
                    Quantity = itemQuantity,
                    PriceBox = itemQuantity * itemPrice
                });
                line = Console.ReadLine();
            }

            boxes = boxes.OrderBy(b => b.PriceBox).ToList();
            boxes.Reverse();

            foreach (var box in boxes)
            {
                Console.WriteLine($"{box.SerialNumber}");
                Console.WriteLine($"-- {box.Item.Name} - ${box.Item.Price:f2}: {box.Quantity}");
                Console.WriteLine($"-- ${box.PriceBox:f2}");

            }
        }
    }
    class Item
    {
        public string Name { get; set; }
        public double Price { get; set; }

    }
    class Box
    {
        public Box()
        {
            Item = new Item();
        }
        public string SerialNumber { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public double PriceBox { get; set; }

    }
}