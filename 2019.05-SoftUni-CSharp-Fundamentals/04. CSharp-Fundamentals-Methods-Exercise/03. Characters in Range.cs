using System;
using System.Linq;
class Program
{
    static void Main()
    {
        char first = char.Parse(Console.ReadLine());
        char second = char.Parse(Console.ReadLine());
        if (first < second)
            CharactersInBetween(first, second);
        else
            CharactersInBetween(second, first);
    }

    static void CharactersInBetween(char start, char end)
    {
        for (int i = start + 1; i < end; i++)
        {
            Console.Write((char)i + " ");
        }
    }
}