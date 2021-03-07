using System;
class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int litres = 255;
        //int added = 0;
        
        for (int i = 0; i < n; i++)
        {
            int added = int.Parse(Console.ReadLine());
            if (added <= litres)
                litres -= added;
            else
            {
                //i--;
                Console.WriteLine("Insufficient capacity!");
            }
        }
        Console.WriteLine(255-litres);
    }
}