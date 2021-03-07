using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    class Program
    {
        static void Main()
        {
            string input = Console.ReadLine();
            var splittedInput = input.Split(">");
            int remainingPower = 0;

            string result = splittedInput[0];

            for(int i = 1; i < splittedInput.Length; i++)
            {
                result += ">";

                string currentString = splittedInput[i];
                char digitSymbol = currentString[0];

                int power = int.Parse(digitSymbol.ToString()) + remainingPower;

                if(power > splittedInput[i].Length)
                {
                    remainingPower = power - currentString.Length;
                }
                else
                {
                    result += splittedInput[i].Substring(power);
                }
            }
            Console.WriteLine(result);
        }
    }
}