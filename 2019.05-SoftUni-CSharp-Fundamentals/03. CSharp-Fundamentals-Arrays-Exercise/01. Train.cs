using System;
using System.Linq;
class Program
{
    static void Main()
    {
        int countOfWagons = int.Parse(Console.ReadLine());
        int[] people = new int[countOfWagons];
        int sum = 0;
        for (int i = 0; i < countOfWagons; i++)
        {
            people[i] = int.Parse(Console.ReadLine());
            sum += people[i];
        }
        string output = String.Join(" ", people);
        Console.WriteLine(output + "\n" + sum);
    }
}