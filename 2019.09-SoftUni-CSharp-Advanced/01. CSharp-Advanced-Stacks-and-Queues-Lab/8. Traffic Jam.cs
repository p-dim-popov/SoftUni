using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var maxCarsToPass = int.Parse(Console.ReadLine());
            var trafficLightQueue = new Queue<string>();
            int count = 0;
            string incoming;
            while ((incoming = Console.ReadLine()) != "end")
            {
                if (!incoming.Equals("green"))
                {
                    trafficLightQueue.Enqueue(incoming);
                }
                else if (incoming.Equals("green"))
                {
                    for (int i = 0; i < maxCarsToPass; i++)
                    {
                        if(trafficLightQueue.Count > 0)
                        { 
                            Console.WriteLine(trafficLightQueue.Dequeue() + " passed!");
                            count++;
                        }
                    }
                }
            }
            Console.WriteLine($"{count} cars passed the crossroads.");
        }
    }
}
