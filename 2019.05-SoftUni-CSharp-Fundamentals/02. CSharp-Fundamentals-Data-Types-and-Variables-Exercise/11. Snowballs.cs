using System;
class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        double highestSnowbalValue = 0;
        int snowballSnow;
        int snowballTime;
        int snowballQuality;

        int s = 0, t = 0, q = 0;


        for (int i = 0; i < n; i++)
        {
            snowballSnow = int.Parse(Console.ReadLine());
            snowballTime = int.Parse(Console.ReadLine());
            snowballQuality = int.Parse(Console.ReadLine());

            double snowballValue = Math.Pow(snowballSnow/snowballTime, snowballQuality);
            
            if (snowballValue > highestSnowbalValue)
            {
                highestSnowbalValue = snowballValue;
                s = snowballSnow;
                t = snowballTime;
                q = snowballQuality;
            }
        }


        Console.WriteLine($"{s} : {t} = {highestSnowbalValue} ({q})");
    }
}