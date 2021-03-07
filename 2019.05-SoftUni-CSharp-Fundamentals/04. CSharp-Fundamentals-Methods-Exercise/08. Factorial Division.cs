using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int number1 = int.Parse(Console.ReadLine());
        int number2 = int.Parse(Console.ReadLine());
        Console.Write("{0:f2}", FactDivision(number1, number2));
    }
    static double FactDivision(int n1, int n2)
    {
        double result = factorial(n1) / factorial(n2);
        return result;
    }
    static double factorial(int n)
    {
        if (n == 0)
            return 1;

        return n * factorial(n - 1);
    }
}