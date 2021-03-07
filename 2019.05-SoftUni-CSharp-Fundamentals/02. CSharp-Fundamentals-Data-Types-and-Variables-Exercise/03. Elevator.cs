using System;
class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int p = int.Parse(Console.ReadLine());

        int courses = n / p;
        if (n % p == 0) ;
        else courses++;
        Console.WriteLine(courses);

    }
}