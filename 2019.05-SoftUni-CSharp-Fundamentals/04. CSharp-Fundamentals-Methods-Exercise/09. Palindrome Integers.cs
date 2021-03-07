using System;
using System.Linq;
class Program
{
    static void Main()
    {
        string input = Console.ReadLine();
        while (input != "END")
        {
            Console.WriteLine(CheckIfPalindrome(input));
            input = Console.ReadLine();
        }
    }
    static string CheckIfPalindrome(string str)
    {
        char[] number = new char[str.Length];
        for (int i = 0; i < str.Length; i++)
        {
            number[str.Length - 1 - i] = (char)str[i];
        }
        string reverseNumber = String.Join("", number);
        if (reverseNumber == str)
            return "true";
        else return "false";
    }
}