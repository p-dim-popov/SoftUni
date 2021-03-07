using System;
class Program
{
    static void Main()
    {
        string a = Console.ReadLine();
        int result = 0;
        for (int i = a.Length; i > 0 ; i--)
        {
            result += a[i-1]-'0';
        }

        Console.WriteLine(result);
    }
}