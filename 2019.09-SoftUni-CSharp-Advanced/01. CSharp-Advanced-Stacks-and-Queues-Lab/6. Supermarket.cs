using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var marketQueue = new Queue<string>();
            string incoming;
            while ((incoming = Console.ReadLine()) != "End")
            {
                if (incoming != "Paid")
                {
                    marketQueue.Enqueue(incoming);
                }
                else
                {
                    while(marketQueue.Count != 0)
                    {
                        Console.WriteLine(marketQueue.Dequeue());
                    }
                }
            }
            Console.WriteLine($"{marketQueue.Count} people remaining.");
        }
    }
}
