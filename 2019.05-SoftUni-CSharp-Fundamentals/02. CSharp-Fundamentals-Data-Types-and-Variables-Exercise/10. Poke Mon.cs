using System;
class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int m = int.Parse(Console.ReadLine());
        int y = int.Parse(Console.ReadLine());
        short counter = 0;
        short N = 0;

        for (int i = n; i >= m; counter++, N = (short)i)
        {
            i -= m;
            if ((decimal)((decimal)n*0.5m) == (decimal)i && y != 0)
                i /= y;
            //else if ((decimal)(n * 0.5m) == (decimal)i && i % y == 0)
        }

        Console.WriteLine(N);
        Console.WriteLine(counter);

    }
}