using System;
using System.Linq;
class Program
{
    static void Main()
    {
        Console.WriteLine(CheckMinNumber());
    }

    static int CheckMinNumber()
    {
        int[] numbers = new int[3];
        numbers[0] = int.Parse(Console.ReadLine());
        numbers[1] = int.Parse(Console.ReadLine());
        numbers[2] = int.Parse(Console.ReadLine());


        int min = numbers[0];
        for (int i = 0; i < 3; i++)
        {
            if (numbers[i] < min)
                min = numbers[i];
        }
        return min;
    }
}