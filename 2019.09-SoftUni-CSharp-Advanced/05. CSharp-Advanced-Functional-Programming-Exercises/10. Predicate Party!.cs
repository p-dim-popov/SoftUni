using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            Func<string, string, bool> isStartingWith = (a, b) => a.StartsWith(b);
            Func<string, string, bool> isEndingWith = (a, b) => a.EndsWith(b);
            Func<int, int, bool> hasSpecifiedLength = (x, y) => x == y;

            Func<string, string, string, bool> appliesToCriteria = (criteriaType, criteriaArg, person) =>
              {
                  switch (criteriaType)
                  {
                      case "StartsWith":
                          return isStartingWith(person, criteriaArg);
                      case "EndsWith":
                          return isEndingWith(person, criteriaArg);
                      case "Length":
                          return hasSpecifiedLength(person.Length, int.Parse(criteriaArg));
                      default:
                          break;
                  }
                  return true;
              };

            Action<List<string>> print = l =>
            {
                if (l.Count > 0)
                    Console.WriteLine(string.Join(", ", l) + " are going to the party!");
                else
                    Console.WriteLine("Nobody is going to the party!");
            };

            var guests = Console.ReadLine().Split().ToList();
            var input = Console.ReadLine();

            while (input != "Party!")
            {
                var commandArgs = input.Split();
                var command = commandArgs[0];
                var criteriaType = commandArgs[1];
                var criteriaArg = commandArgs[2];

                switch (command)
                {
                    case "Double":
                        for (int i = 0; i < guests.Count; i++)
                        {
                            if (appliesToCriteria(criteriaType, criteriaArg, guests[i]))
                            {
                                guests.Insert(i, guests[i++]);
                            }
                        }
                        break;
                    case "Remove":
                        for (int i = 0; i < guests.Count; i++)
                        {
                            guests = guests.Distinct().ToList();
                            if (appliesToCriteria(criteriaType, criteriaArg, guests[i]))
                            {
                                guests.RemoveAt(i--);
                            }
                        }
                        break;
                    default:
                        break;
                }

                input = Console.ReadLine();
            }
            print(guests);
        }
    }
}
