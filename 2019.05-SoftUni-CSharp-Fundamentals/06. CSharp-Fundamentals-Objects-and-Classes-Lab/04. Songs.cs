using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Numerics;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            int numSongs = int.Parse(Console.ReadLine());
            var songs = new List<Song>();
            for (int i = 0; i < numSongs; i++)
            {
                string[] data = Console.ReadLine().Split("_");

                string type = data[0];
                string name = data[1];
                string time = data[2];

                var song = new Song();

                song.TypeList = type;
                song.Name = name;
                song.Time = time;

                songs.Add(song);
            }
            string typeList = Console.ReadLine();

            if (typeList == "all")
            {
                foreach (var song in songs)
                {
                    Console.WriteLine(song.Name);
                }
            }
            else
            {
                var filteredSongs = songs
                    .Where(s => s.TypeList == typeList)
                    .ToList();
                foreach (var song in filteredSongs)
                {
                        Console.WriteLine(song.Name);
                }
            }
        }
    }

    class Song
    {
        public string TypeList { get; set; }

        public string Name { get; set; }

        public string Time { get; set; }

    }
}