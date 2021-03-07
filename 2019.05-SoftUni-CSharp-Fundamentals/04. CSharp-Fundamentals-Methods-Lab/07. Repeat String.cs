using System;
using System.Linq;
class Program
{
    static void Main()
    {
        string str = Console.ReadLine();
        int n = int.Parse(Console.ReadLine());
        Console.WriteLine(RepeatString(str, n));
    }

    private static string RepeatString(string str, int n)
    {
        string[] result = new string[n];

        for (int i = 0; i < n; i++)
        {
            result[i] = str;
        }
        string realResult = String.Join("", result);
        return realResult;
    }
}