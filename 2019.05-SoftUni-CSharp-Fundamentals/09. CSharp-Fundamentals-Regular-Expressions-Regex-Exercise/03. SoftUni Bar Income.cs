using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Program
{
    class Program
    {
        static void Main()
        {
            string line = Console.ReadLine();
            double totalIncome = 0;
            while (line != "end of shift")
            {
                var regex = new Regex(@"^.*\%(?<name>[A-Z][a-z]+)\%.*\<(?<product>\w+)\>.*\|(?<quantity>\d+)\|(?<price>(?:\d+.\d+|\d+))\$.*$");
                if (regex.IsMatch(line))
                {
                    var lineMatch = regex.Match(line);
                    string name = lineMatch.Groups["name"].Value;
                    string product = lineMatch.Groups["product"].Value;
                    int quantity = int.Parse(lineMatch.Groups["quantity"].Value);
                    double price = double.Parse(lineMatch.Groups["price"].Value);

                    totalIncome += price * quantity;
                    Console.WriteLine($"{name}: {product} - {quantity * price:f2}");
                }
                line = Console.ReadLine();
            }
            Console.WriteLine($"Total income: {totalIncome:f2}");
        }
    }
}