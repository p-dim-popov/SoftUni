using System;
class Program
{
    static void Main()
    {
        byte c = byte.Parse(Console.ReadLine());
        int y = c * 100;
        int d = (int)(y * 365.2422);
        int h = d * 24;
        int m = h * 60;

        Console.WriteLine($"{c} centuries = {y:f0} years = {d:f0} days = {h:f0} hours = {m:f0} minutes");
    }
}