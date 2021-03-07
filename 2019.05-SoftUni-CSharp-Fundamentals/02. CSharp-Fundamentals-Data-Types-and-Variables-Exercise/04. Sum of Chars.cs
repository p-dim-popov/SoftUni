using System;
class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        char ch = ' ';
        int sum = 0;
        for (int i = 0; i < n; i++)
        {
            ch = char.Parse(Console.ReadLine());
            sum += (int)ch;
        }
        Console.WriteLine($"The sum equals: {sum}");
    }
}