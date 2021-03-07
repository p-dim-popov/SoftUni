using System;
class Program
{
    static void Main()
    {
        double m = double.Parse(Console.ReadLine());
        m /= 1000;
        Console.WriteLine($"{m:f2}");
    }
}