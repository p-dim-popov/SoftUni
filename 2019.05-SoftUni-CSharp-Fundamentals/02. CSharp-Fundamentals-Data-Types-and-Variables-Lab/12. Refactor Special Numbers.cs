using System;
class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        bool isStrong = false;
        for (int i = 1; i <= n; i++)
        {
            int sum = 0;
            for(int j = i; j > 0; j /= 10)
                sum += j % 10;
            isStrong = (sum == 5) || (sum == 7) || (sum == 11);
            Console.WriteLine("{0} -> {1}", i, isStrong);
        }
    }
}