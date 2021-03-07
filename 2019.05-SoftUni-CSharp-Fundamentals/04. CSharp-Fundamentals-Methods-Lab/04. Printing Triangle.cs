using System;
using System.Linq;
class Program
{
    static void DrawUpperPyramid(int a)
    {
        for (int i = 1; i <= a; i++)
        {
            for (int j = 1; j <= i; j++)
            {
                Console.Write(j + " ");
            }
            Console.WriteLine();
        }
    }
    static void DrawLowerPyramid(int a)
    {
        for (int i = 1; i <= a; a--)
        {
            for (int j = 0; j < a-1; j++)
            {
                Console.Write((j+1) + " ");
            }
            Console.WriteLine();
        }
    }


    static void Main(string[] args)
    {
        int a = int.Parse(Console.ReadLine());

        DrawUpperPyramid(a);
        DrawLowerPyramid(a);

    }
}