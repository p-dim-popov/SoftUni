using System;
class Program
{
    static void Main()
    {
        decimal n = decimal.Parse(Console.ReadLine());
        decimal m = 0m;
        decimal sum = 0m;
        for (int i = 0; i < n; i++)
        {
            m = decimal.Parse(Console.ReadLine());
            sum += m;
        }
        Console.WriteLine(sum);
    }
}