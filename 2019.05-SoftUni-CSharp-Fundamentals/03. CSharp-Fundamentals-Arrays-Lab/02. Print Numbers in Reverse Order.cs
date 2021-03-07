using System;
class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int[] numbers = new int[n];

        for (int i = n; i > 0; i--)
        {
            numbers[i - 1] = int.Parse(Console.ReadLine());
        }
        for (int j = 0; j < numbers.Length; j++)
        {
            Console.Write($"{numbers[j]} ");
        }
    }
}