using System;
class Program
{
    static void Main()
    {
        double pounds = double.Parse(Console.ReadLine());
        pounds *= 1.31;
        Console.WriteLine($"{pounds:f3}");
    }
}