using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSoftUni
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfCommands = int.Parse(Console.ReadLine());

            var users = new Dictionary<string, string>();


            for (int i = 0; i < numberOfCommands; i++)
            {
                var commandArgs = Console.ReadLine()
                    .Split(" ");

                string command = commandArgs[0];
                string name = commandArgs[1];

                if(command == "register")
                {
                    string plateNumber = commandArgs[2];

                    if(!users.ContainsKey(name))
                    {
                        users[name] = plateNumber;

                        Console.WriteLine($"{name} registered {plateNumber} successfully");
                    }
                    else
                    {
                        Console.WriteLine($"ERROR: already registered with plate number {plateNumber}");
                    }
                }
                else if(command == "unregister")
                {
                    if(!users.ContainsKey(name))
                    {
                        Console.WriteLine($"ERROR: user {name} not found");
                    }
                    else
                    {
                        Console.WriteLine($"{name} unregistered successfully");
                        users.Remove(name);
                    }
                }
            }
            foreach(var kvp in users)
            {
                Console.WriteLine($"{kvp.Key} => {kvp.Value}");
            }
        }
    }
}
