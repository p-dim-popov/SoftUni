using System;
class Program
{
    static void Main()
    {
        string clientInput = Console.ReadLine();
        double money = 0;
        double sumOfMoney = 0;

        while (clientInput != "Start")
        {
            money = double.Parse(clientInput);

            switch (money)
            {
                case 0.1:
                case 0.2:
                case 0.5:
                case 1:
                case 2:
                    sumOfMoney += money;
                    break;
                default:
                    Console.WriteLine("Cannot accept {0}", money);
                    break;
            }

            clientInput = Console.ReadLine();
        }

        string chosenProduct = Console.ReadLine();
        double sumOfProducts = 0;

        while (chosenProduct != "End")
        {
            switch (chosenProduct)
            {
                case "Nuts":
                    sumOfProducts += 2.0;
                    if (sumOfProducts <= sumOfMoney)
                        Console.WriteLine("Purchased nuts");
                    else
                    {
                        Console.WriteLine("Sorry, not enough money");
                        sumOfProducts -= 2.0;
                    }
                    break;
                case "Water":
                    sumOfProducts += 0.7;
                    if (sumOfProducts <= sumOfMoney)
                        Console.WriteLine("Purchased water");
                    else
                    {
                        Console.WriteLine("Sorry, not enough money");
                        sumOfProducts -= 0.7;
                    }
                    break;
                case "Crisps":
                    sumOfProducts += 1.5;
                    if (sumOfProducts <= sumOfMoney)
                        Console.WriteLine("Purchased crisps");
                    else
                    {
                        Console.WriteLine("Sorry, not enough money");
                        sumOfProducts -= 1.5;
                    }
                    break;
                case "Soda":
                    sumOfProducts += 0.8;
                    if (sumOfProducts <= sumOfMoney)
                        Console.WriteLine("Purchased soda");
                    else
                    {
                        Console.WriteLine("Sorry, not enough money");
                        sumOfProducts -= 0.8;
                    }
                    break;
                case "Coke":
                    sumOfProducts += 1.0;
                    if (sumOfProducts <= sumOfMoney)
                        Console.WriteLine("Purchased coke");
                    else
                    {
                        Console.WriteLine("Sorry, not enough money");
                        sumOfProducts -= 1.0;
                    }
                    break;
                default:
                    Console.WriteLine("Invalid product");  
                    break;
            }

            chosenProduct = Console.ReadLine();
        }

        Console.WriteLine($"Change: {sumOfMoney - sumOfProducts:f2}");
    }
}