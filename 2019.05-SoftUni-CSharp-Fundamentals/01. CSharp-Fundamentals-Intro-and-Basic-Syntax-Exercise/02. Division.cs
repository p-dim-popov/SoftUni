using System;
class Program
{
    static void Main()
    {
        short n = short.Parse(Console.ReadLine());
        short a = 0;

        if (n % 2 == 0) a=2;
        if (n % 3 == 0) a=3;
        if (n % 6 == 0) a=6;
        if (n % 7 == 0) a=7;
        if (n % 10 == 0) a=10;

        switch (a)
        {
            case 2:
                Console.WriteLine("The number is divisible by 2");
                break;
            case 3:
                Console.WriteLine("The number is divisible by 3");
                break;
            case 6:
                Console.WriteLine("The number is divisible by 6");
                break;
            case 7:
                Console.WriteLine("The number is divisible by 7");
                break;
            case 10:
                Console.WriteLine("The number is divisible by 10");
                break;
            default:
                Console.WriteLine("Not divisible");
                break;
        }

    }
}