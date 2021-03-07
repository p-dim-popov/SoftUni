using System;
class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        string oldModel = string.Empty;
        double oldVolume = 0;

        for (int i = 0; i < n; i++)
        {
            string model = Console.ReadLine();
            double radius = double.Parse(Console.ReadLine());
            int height = int.Parse(Console.ReadLine());
            double volume = Math.PI * Math.Pow(radius, 2) * height;

            if (volume > oldVolume)
            {
                oldVolume = volume;
                oldModel = model;
            }
        }

        Console.WriteLine(oldModel);
    }
}