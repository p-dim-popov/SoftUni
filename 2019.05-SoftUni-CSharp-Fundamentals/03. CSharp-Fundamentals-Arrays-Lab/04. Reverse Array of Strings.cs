using System;
using System.Linq;
class Program
{
    static void Main()
    {
        string[] texts = Console.ReadLine().Split();

        string[] textArray = new string[texts.Length];

        for (int i = texts.Length - 1; i >= 0; i--)
        {
            textArray[texts.Length - 1 - i] = texts[i];
        }
        string result = string.Join(" ", textArray);
        Console.Write(result);
    }
}