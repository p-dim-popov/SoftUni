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
            string regexTest = @">>(?<name>[A-z]+)<<(?<price>[0-9]+(\.[0-9]+)*)\!(?<quantity>[0-9]+)";
            var list = new List<string>();
            double sum = 0;
            string line = Console.ReadLine();

            while(line != "Purchase")
            {
                var match = Regex.Match(line, regexTest);
                if (Regex.IsMatch(line, regexTest))
                {
                    string name = match.Groups["name"].Value;
                    double price = double.Parse(match.Groups["price"].Value);
                    int quantity = int.Parse(match.Groups["quantity"].Value);
                    list.Add(name);
                    sum += price * quantity;
                }
                
                line = Console.ReadLine();
            }
            Console.WriteLine("Bought furniture:");
            foreach(var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"Total money spend: {sum:f2}");
        }
    }
}