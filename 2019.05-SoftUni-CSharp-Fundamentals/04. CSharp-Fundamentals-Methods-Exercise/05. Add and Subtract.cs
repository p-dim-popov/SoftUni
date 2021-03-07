using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int[] number = new int[3];
        number[0] = int.Parse(Console.ReadLine());
        number[1] = int.Parse(Console.ReadLine());
        number[2] = int.Parse(Console.ReadLine());
        Console.WriteLine(SUBTRACT(number[2], SUM(number[0], number[1])));
    }
    static int SUM(int number1, int number2)
    {
        int result = number1 + number2;
        return result;
    }
    static int SUBTRACT(int third, int sum)
    {
        int result = sum - third;
        return result;
    }
}