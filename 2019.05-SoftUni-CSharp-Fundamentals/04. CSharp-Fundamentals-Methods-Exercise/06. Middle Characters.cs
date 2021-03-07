using System;
using System.Linq;
class Program
{
    static void Main()
    {
        string str = Console.ReadLine();
        if (str.Length % 2 == 0)
            Console.Write(MiddleCharEven(str));
        else
            Console.Write(MiddleCharOdd(str));
    }
    static string MiddleCharEven(string str)
    {
        string middleChar;
        char[] evenCase = { str[(str.Length / 2) - 1], str[str.Length / 2] };
        middleChar = String.Join("", evenCase);
        return middleChar;
    }
    static string MiddleCharOdd(string str)
    {
        string middleChar;
        middleChar = String.Join("", str[str.Length / 2]);
        return middleChar;
    }
}