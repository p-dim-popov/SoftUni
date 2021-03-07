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
            var input = Console.ReadLine()
                .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var ticket in input)
            {
                if (ticket.Length != 20)
                {
                    Console.WriteLine("invalid ticket");
                    continue;
                }
                string leftSide = ticket.Substring(0, 10);
                string rightSide = ticket.Substring(10);
                var regex = new Regex(@"([\@\#\$\^]{6,})");
                if (regex.IsMatch(leftSide) && regex.IsMatch(rightSide))
                {
                    if (Regex.IsMatch(ticket, @"\@{20}|\#{20}|\${20}|\^{20}"))
                    {
                        Console.WriteLine($"ticket \"{ticket}\" - 10{ticket[0]} Jackpot!");
                        continue;
                    }
                    var regexForMatchesCount = new Regex(@"\@{6,9}|\${6,9}|\^{6,9}|\#{6,9}");
                    var rightCount = regexForMatchesCount.Match(rightSide).Length;
                    var leftCount = regexForMatchesCount.Match(leftSide).Length;
                    if (leftCount > 5 && rightCount > 5)
                    {
                        //this is slower: count = (leftCount > rightCount) ? rightCount : leftCount;
                        int count = (leftCount > rightCount) ? count = rightCount : count = leftCount;
                        if (leftSide[5].Equals(rightSide[5]))
                        {
                            Console.WriteLine($"ticket \"{ticket}\" - {count}{ticket[5]}");
                        }
                        else
                        {
                            Console.WriteLine($"ticket \"{ticket}\" - no match");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"ticket \"{ticket}\" - no match");
                    }
                }
                else
                {
                    Console.WriteLine($"ticket \"{ticket}\" - no match");
                }
            }
        }
    }
}