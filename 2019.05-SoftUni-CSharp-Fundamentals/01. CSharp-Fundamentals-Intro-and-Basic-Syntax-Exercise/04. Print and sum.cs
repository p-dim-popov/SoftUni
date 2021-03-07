using System;
class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int m = int.Parse(Console.ReadLine());
        int sum = 0;

        for (int i = n; i <= m; i++)
        {
            Console.Write($"{i} ");
            sum += i;
        }
        Console.WriteLine();
        Console.WriteLine($"Sum: {sum}");
    }
}