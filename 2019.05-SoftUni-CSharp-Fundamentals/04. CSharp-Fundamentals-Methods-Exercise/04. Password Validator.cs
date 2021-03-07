using System;
using System.Linq;
class Program
{
    static void Main()
    {
        string password = Console.ReadLine();
        bool outOfRange = OutOfRange(password);
        bool exclusiveCharacters = ExclusiveCharacters(password);
        bool underTwoDigits = UnderTwoDigits(password);

        if (outOfRange)
            Console.WriteLine("Password must be between 6 and 10 characters");
        if (exclusiveCharacters)
            Console.WriteLine("Password must consist only of letters and digits");
        if (underTwoDigits)
            Console.WriteLine("Password must have at least 2 digits");
        if (!outOfRange && !exclusiveCharacters && !underTwoDigits)
            Console.WriteLine("Password is valid");
    }
    static bool OutOfRange(string pass)
    {
        if (pass.Length < 6 || pass.Length > 10)
            return true;
        else return false;
    }
    static bool ExclusiveCharacters(string pass)
    {
        for (int i = 0; i < pass.Length; i++)
        {
            if ((pass[i] > '9' || pass[i] < '0') && (pass[i] > 'z' || pass[i] < 'a') && (pass[i] > 'Z' || pass[i] < 'A'))
                return true;
        }
        return false;
    }
    static bool UnderTwoDigits(string pass)
    {
        for (int i = 0, digitsCount = 0; i < pass.Length; i++)
        {
            if (pass[i] >= '0' && pass[i] <= '9')
                digitsCount++;
            if (digitsCount >= 2)
                return false;
        }
        return true;
    }
}