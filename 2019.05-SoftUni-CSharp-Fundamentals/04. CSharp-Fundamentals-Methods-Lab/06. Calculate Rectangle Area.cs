using System;
using System.Linq;
class Program
{
    static void Main()
    {
        double width = double.Parse(Console.ReadLine());
        double height = double.Parse(Console.ReadLine());
        double area = RectangleArea(width, height);
        Console.WriteLine(area);
    }

    static double RectangleArea(double width, double height)
    {
        return width * height;
    }
}