using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        PrintNxNmatrix(n);
    }
    static void PrintNxNmatrix(int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(n + " ");
            }
            Console.WriteLine();
        }
    }
    
}