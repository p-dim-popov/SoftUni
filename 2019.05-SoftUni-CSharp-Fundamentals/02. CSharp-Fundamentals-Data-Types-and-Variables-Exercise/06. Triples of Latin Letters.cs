using System;
class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        char word = 'a';
        
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                for (int k = 0; k < n; k++)
                {
                    Console.WriteLine($"{(char)(i + 'a')}{(char)(j + 'a')}{(char)(k + 'a')}");
                }
            }
        }
    }
}