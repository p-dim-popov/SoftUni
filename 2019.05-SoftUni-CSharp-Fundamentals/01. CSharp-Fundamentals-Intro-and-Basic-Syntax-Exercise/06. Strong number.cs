using System;
class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int sum = 0;
        int factorial = 1;

        for (int i = n; i != 0; i/=10, factorial = 1)
        {
            for (int j = 1; j <= i%10; j++)
            {
                factorial *= j;
            }
            sum += factorial;
        }

        if (sum == n) Console.WriteLine("yes");
        else Console.WriteLine("no");
    }
}