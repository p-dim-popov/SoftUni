using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    class Program
    {
        public static Dictionary<string, List<string>> forceBook = new Dictionary<string, List<string>>();
        static void Main()
        {


            string line = Console.ReadLine();

            while (line != "Lumpawaroo")
            {
                var action = line.Split(" ").ToArray();
                if (action.Contains("|"))
                {
                    var args = line.Split(" | ");
                    string forceSide = args[0];
                    string forceUser = args[1];

                    AddToTheSide(forceSide, forceUser);
                }
                else if (action.Contains("->"))
                {
                    var args = line.Split(" -> ");
                    string forceUser = args[0];
                    string forceSide = args[1];

                    ChangeTheSide(forceUser, forceSide);
                }
                line = Console.ReadLine();
            }
            forceBook = forceBook
                .OrderByDescending(x => x.Value.Count)
                .ThenBy(x => x.Key)
                .Where(x => x.Value.Any())
                .ToDictionary(x => x.Key, y => y.Value);
            foreach(var kvp in forceBook)
            {
                forceBook[kvp.Key].Sort();
                var tempList = forceBook[kvp.Key];
                Console.WriteLine($"Side: {kvp.Key}, Members: {tempList.Count}");
                tempList.ForEach(x => Console.WriteLine($"! {x}"));
            }
        }
        public static void AddToTheSide(string side, string user)
        {
            foreach (var kvp in forceBook)
            {
                if (kvp.Value.Contains(user))
                {
                    return;
                }
            }
            if (!forceBook.ContainsKey(side))
            {
                forceBook[side] = new List<string>();
            }
            if (!forceBook[side].Contains(user))
            {
                forceBook[side].Add(user);
            }
            return;
        }
        public static void ChangeTheSide(string user, string side)
        {
            foreach (var kvp in forceBook)
            {
                if (kvp.Value.Contains(user))
                {
                    kvp.Value.Remove(user);
                }
            }
            if (!forceBook.ContainsKey(side))
            {
                forceBook[side] = new List<string>();
            }
            if (!forceBook[side].Contains(user))
            {
                forceBook[side].Add(user);
            }
            AddToTheSide(side, user);
            Console.WriteLine($"{user} joins the {side} side!");
            return;
        }
    }
}