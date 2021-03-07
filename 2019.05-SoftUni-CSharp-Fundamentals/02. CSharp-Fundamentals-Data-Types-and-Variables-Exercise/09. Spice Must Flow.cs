using System;
class Program
{
    static void Main()
    {
        uint sYield= uint.Parse(Console.ReadLine());

        ushort countDays = 0;
        int spiceCollected = 0;

        for (uint i = sYield; i >= 100; i -= 10)
        {
            spiceCollected += (int)i - 26;
            if (spiceCollected < 0)
                spiceCollected += 26;
            countDays++;
            //if ((--i) - 26 < 100) break;
        }

        if (spiceCollected >= 26)
            spiceCollected -= 26;
        Console.WriteLine(countDays);
        Console.WriteLine(spiceCollected);


    }
}