using System;
class Program
{
    static void Main()
    {
        string first = Console.ReadLine();
        string last = Console.ReadLine();
        string delimiter = Console.ReadLine();

        Console.WriteLine($"{first}{delimiter}{last}");
    }
}