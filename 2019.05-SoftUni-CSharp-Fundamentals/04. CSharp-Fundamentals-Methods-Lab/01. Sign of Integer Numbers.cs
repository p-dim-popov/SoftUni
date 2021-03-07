using System;
using System.Linq;
class Program
{
    static void PrintSignOfInt(int number)
    {
        if (number > 0) Console.WriteLine("The number {0} is positive.", number);
        else if (number < 0) Console.WriteLine("The number {0} is negative.", number);
        else Console.WriteLine("The number {0} is zero.", number);
    }

    static void Main()
    {
        PrintSignOfInt(int.Parse(Console.ReadLine()));
    }
}