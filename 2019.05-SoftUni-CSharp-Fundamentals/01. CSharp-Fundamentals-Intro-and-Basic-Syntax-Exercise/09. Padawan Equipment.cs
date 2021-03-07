using System;
class Program
{
    static void Main()
    {
        float amountOfMoney = float.Parse(Console.ReadLine());
        short studentsCount = short.Parse(Console.ReadLine());
        float priceSSabre = float.Parse(Console.ReadLine());
        float priceSRobe = float.Parse(Console.ReadLine());
        float priceSBelt = float.Parse(Console.ReadLine());

        float countBelts = studentsCount - (studentsCount/6);
        float countSabres = (float)Math.Ceiling(studentsCount * 1.1);

        double sum = priceSSabre*countSabres + priceSRobe*studentsCount + countBelts*priceSBelt;

        if (sum <= amountOfMoney)
            Console.WriteLine($"The money is enough - it would cost {sum:f2}lv.");
        else
            Console.WriteLine($"Ivan Cho will need {sum - amountOfMoney:f2}lv more.");


    }
}