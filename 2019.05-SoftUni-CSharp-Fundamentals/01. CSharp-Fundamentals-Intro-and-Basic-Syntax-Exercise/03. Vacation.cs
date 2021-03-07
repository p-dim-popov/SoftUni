using System;
class Program
{
    static void Main()
    {
        int numberOfPeople = int.Parse(Console.ReadLine());
        string typeOfPeople = Console.ReadLine();
        string day = Console.ReadLine();

        double price = 0;
        double preFinal = 0;

        switch (day)
        {
            case "Friday":
                switch (typeOfPeople)
                {
                    case "Students":
                        price = 8.45;
                        break;
                    case "Business":
                        price = 10.90;
                        break;
                    case "Regular":
                        price = 15;
                        break;
                }
                break;
            case "Saturday":
                switch (typeOfPeople)
                {
                    case "Students":
                        price = 9.80;
                        break;
                    case "Business":
                        price = 15.60;
                        break;
                    case "Regular":
                        price = 20;
                        break;
                }
                break;
            case "Sunday":
                switch (typeOfPeople)
                {
                    case "Students":
                        price = 10.46;
                        break;
                    case "Business":
                        price = 16;
                        break;
                    case "Regular":
                        price = 22.50;
                        break;
                }
                break;
        }

        preFinal = numberOfPeople * price;

        switch (typeOfPeople)
        {
            case "Students":
                if (numberOfPeople >= 30) preFinal *= 0.85;
                break;
            case "Business":
                if (numberOfPeople >= 100) preFinal = (numberOfPeople - 10.0) * price;
                break;
            case "Regular":
                if (numberOfPeople >= 10 && numberOfPeople <= 20) preFinal *= 0.95;
                break;
        }

        Console.WriteLine($"Total price: {preFinal:f2}");

    }
}