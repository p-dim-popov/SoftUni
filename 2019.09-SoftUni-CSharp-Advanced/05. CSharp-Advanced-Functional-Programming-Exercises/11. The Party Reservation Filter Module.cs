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
            var line = Console.ReadLine().Split();
            var guests = new List<KeyValuePair<string, UInt16>>();
            foreach (var guest in line)
            {
                guests.Add(new KeyValuePair<string, ushort>(guest, 1));
            }
            string input;
            while ((input = Console.ReadLine()) != "Print")
            {
                var splittedInput = input.Split(';');
                var command = splittedInput[0];
                var filterType = splittedInput[1];
                var filterArgs = splittedInput[2];

                FilterGuests(command, filterType, filterArgs, guests);
            }
            guests.Where(x => x.Value == 1).ToList().ForEach(x => Console.Write(x.Key + " "));
        }
        private static void FilterGuests(string command, string filterType, string filterArgs, List<KeyValuePair<string, ushort>> guests)
        {
            Action<List<KeyValuePair<string, UInt16>>, int> hideGuest = (a, i) => a[i] = new KeyValuePair<string, ushort>(a[i].Key, 0);
            Action<List<KeyValuePair<string, UInt16>>, int> showGuest = (a, i) => a[i] = new KeyValuePair<string, ushort>(a[i].Key, 1);

            switch (command)
            {
                case "Add filter":
                    for (int i = 0; i < guests.Count; i++)
                    {
                        switch (filterType)
                        {
                            case "Starts with":
                                if (guests[i].Key.StartsWith(filterArgs))
                                    hideGuest(guests, i);
                                break;
                            case "Ends with":
                                if (guests[i].Key.EndsWith(filterArgs))
                                    hideGuest(guests, i); break;
                            case "Length":
                                if (guests[i].Key.Length == int.Parse(filterArgs))
                                    hideGuest(guests, i); break;
                            case "Contains":
                                if (guests[i].Key.Contains(filterArgs))
                                    hideGuest(guests, i); break;
                            default:
                                break;
                        }
                    }
                    break;
                case "Remove filter":
                    for (int i = 0; i < guests.Count; i++)
                    {
                        switch (filterType)
                        {
                            case "Starts with":
                                if (guests[i].Key.StartsWith(filterArgs))
                                    showGuest(guests, i);
                                break;
                            case "Ends with":
                                if (guests[i].Key.EndsWith(filterArgs))
                                    showGuest(guests, i);
                                break;
                            case "Length":
                                if (guests[i].Key.Length == int.Parse(filterArgs))
                                    showGuest(guests, i);
                                break;
                            case "Contains":
                                if (guests[i].Key.Contains(filterArgs))
                                    showGuest(guests, i);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
