using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            PrintIfTopNumber($"{i}");
        }
    }
    static void PrintIfTopNumber(string number)
    {
        int[] topArray = new int[number.Length];
        for (int i = 0; i < number.Length; i++)
        {
            int current = int.Parse($"{number[i]}");
            topArray[i] = current;
        }
        if (topArray.Sum() % 8 == 0)
        {
            for (int i = 0; i < topArray.Length; i++)
            {
                if (topArray[i] % 2 != 0)
                {
                    Console.WriteLine(String.Join("", topArray));
                    return;
                }
            }
        }
    }
}