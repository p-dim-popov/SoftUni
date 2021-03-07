using System;
class Program
{
    static void Main()
    {
        short n = short.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                Console.Write($"{i + 1} ");
            }
            Console.WriteLine("");
        }
    }
}