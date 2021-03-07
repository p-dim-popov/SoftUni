using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    class Program
    {
        static void Main()
        {
            var path = Console.ReadLine()
                .Split("\\")
                .ToArray();
            var fileAndExtension = String.Join("", path.TakeLast(1))
                .Split(".")
                .ToArray();
            Console.WriteLine($"File name: {fileAndExtension[0]}\n" + 
                              $"File extension: {fileAndExtension[1]}");
        }
    }
}