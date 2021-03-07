using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    class Program
    {
        static void Main()
        {
            var companyUsers = new Dictionary<string, List<string>>();

            string line = Console.ReadLine();
            while(line != "End")
            {
                var input = line
                    .Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
                string companyName = input[0];
                string userName = input[1];

                if(!companyUsers.ContainsKey(companyName))
                {
                    companyUsers[companyName] = new List<string>();
                }

                if (!companyUsers[companyName].Contains(userName))
                {
                    companyUsers[companyName].Add(userName);
                }

                line = Console.ReadLine();
            }

            companyUsers = companyUsers
                .OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);
            foreach(var kvp in companyUsers)
            {
                Console.WriteLine($"{kvp.Key}");
                foreach(var user in kvp.Value)
                {
                    Console.WriteLine($"-- {user}");
                }
            }
        }
    }
}