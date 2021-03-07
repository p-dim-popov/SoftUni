using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            var box = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));
            int rackCapacity = int.Parse(Console.ReadLine());
            int sum = 0;
            int rackCount = 1;
            while(box.Count != 0)
            {
                int currentCloth = box.Pop();
                sum += currentCloth;
                if(sum > rackCapacity)
                {
                    sum = currentCloth;
                    rackCount++;
                }
            }
            Console.WriteLine(rackCount);
        }
    }
}
