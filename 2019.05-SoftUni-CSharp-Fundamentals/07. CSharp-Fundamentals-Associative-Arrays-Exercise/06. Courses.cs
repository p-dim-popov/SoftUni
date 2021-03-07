using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSoftUni
{
    class Program
    {
        static void Main()
        {
            var courses = new Dictionary<string, List<string>>();

            string line = Console.ReadLine();

            while (line != "end")
            {
                var args = line
                    .Split(" : ", StringSplitOptions.RemoveEmptyEntries);
                string courseName = args[0];
                string studentName = args[1];
                if (!courses.ContainsKey(courseName))
                {
                    courses[courseName] = new List<string>();
                }
                courses[courseName].Add(studentName);

                line = Console.ReadLine();
            }
            courses = courses
                .OrderByDescending(x => x.Value.Count)
                .ToDictionary(x => x.Key, x => x.Value);
            foreach(var kvp in courses)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value.Count}");
                var studentsList = kvp.Value
                    .OrderBy(x => x)
                    .ToList();
                foreach(var student in studentsList)
                {
                    Console.WriteLine($"-- {student}");
                }
            }
        }
    }
}

