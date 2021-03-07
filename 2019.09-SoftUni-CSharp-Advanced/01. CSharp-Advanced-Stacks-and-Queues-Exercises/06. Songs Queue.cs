using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            var playlist = new Queue<string>(Console.ReadLine().Split(", "));
            do
            {
                var input = new Queue<string>(Console.ReadLine().Split());
                string command = input.Peek();
                if (command == "Play")
                {
                    playlist.Dequeue();
                }
                else if (command == "Add")
                {
                    input.Dequeue();
                    string song = string.Join(' ', input);
                    if (!playlist.Contains(song))
                    {
                        playlist.Enqueue(song);
                    }
                    else
                    {
                        Console.WriteLine(song + " is already contained!");
                    }
                }
                else if (command == "Show")
                {
                    Console.WriteLine(String.Join(", ", playlist));
                }
            } while (playlist.Count != 0);            
            Console.WriteLine("No more songs!");
        }
    }
}
