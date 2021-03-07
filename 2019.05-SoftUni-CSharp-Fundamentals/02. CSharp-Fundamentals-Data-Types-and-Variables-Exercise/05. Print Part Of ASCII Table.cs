using System;
class Program
{
    static void Main()
    {
        int min = int.Parse(Console.ReadLine());
        int max = int.Parse(Console.ReadLine());
        char n = ' ';

        for (int i = min; i <= max; i++)
        {
            n = (char)i;
            Console.Write($"{n} ");
        }
    }
}