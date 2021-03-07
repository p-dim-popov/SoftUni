using System;
using System.Linq;
class Program
{
    static void Main()
    {
        string type = Console.ReadLine();
        int quantity = int.Parse(Console.ReadLine());
        PrintTotalPrice(type, quantity);
    }

    static void PrintTotalPrice(string type, float q)
    {
        switch (type)
        {
            case "coffee":
                Console.WriteLine($"{q * 1.5:f2}");
                break;
            case "water":
                Console.WriteLine($"{q * 1.0:f2}");
                break;
            case "coke":
                Console.WriteLine($"{q * 1.4:f2}");
                break;
            case "snacks":
                Console.WriteLine($"{q * 2.0:f2}");
                break;

        }
    }
}