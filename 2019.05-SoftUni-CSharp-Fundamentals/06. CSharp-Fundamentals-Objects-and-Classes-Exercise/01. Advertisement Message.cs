using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        string[] phrases =
        {
            "Excellent product.",
            "Such a great product.",
            "I always use that product.",
            "Best product of its category.",
            "Exceptional product.",
            "I can’t live without this product."
        };

        string[] events =
        {
            "Now I feel good.",
            "I have succeeded with this product.",
            "Makes miracles. I am happy of the results!",
            "I cannot believe but now I feel awesome.",
            "Try it yourself, I am very satisfied.",
            "I feel great!"
        };

        string[] authors =
        {
            "Diana",
            "Petya",
            "Stella",
            "Elena",
            "Katya",
            "Iva",
            "Annie",
            "Eva"
        };

        string[] cities =
        {
            "Burgas",
            "Sofia",
            "Plovdiv",
            "Varna",
            "Ruse"
        };


        

        for (int i = 0; i  < n; i++)
        {
            var rand = new Random();
            string randomPhrase = phrases[rand.Next(0, phrases.Length)];
            string randomEvent = events[rand.Next(0, events.Length)];
            string randomAuthor = authors[rand.Next(0, authors.Length)];
            string randomCity = authors[rand.Next(cities.Length)];

            Console.WriteLine($"{randomPhrase} {randomEvent} {randomAuthor} - {randomCity}");
        }

    }
}