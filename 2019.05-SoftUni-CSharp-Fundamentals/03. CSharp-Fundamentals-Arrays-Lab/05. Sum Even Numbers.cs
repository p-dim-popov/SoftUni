using System;
using System.Linq;
class Program
{
    static void Main()
    {
        string[] text = Console.ReadLine().Split();
        int sum = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (int.Parse(text[i])%2 == 0)
                sum += int.Parse(text[i]);
        }
        Console.Write(sum);
    }
}