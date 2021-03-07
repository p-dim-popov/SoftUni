using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSoftUni
{
    class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            var studentsWithGrades = new Dictionary<string, List<double>>();
            for(int i = 0; i < n; i++)
            {
                string studentName = Console.ReadLine();
                double studentGrade = double.Parse(Console.ReadLine());
                if(!studentsWithGrades.ContainsKey(studentName))
                {
                    studentsWithGrades[studentName] = new List<double>();
                }
                studentsWithGrades[studentName].Add(studentGrade);
            }
            var filteredStudents = studentsWithGrades
                .Where(x => x.Value.Average() >= 4.5)
                .OrderByDescending(x => x.Value.Average())
                .ToDictionary(x => x.Key, x => x.Value);
            foreach(var student in filteredStudents)
            {
                Console.WriteLine($"{student.Key} -> {student.Value.Average():f2}");
            }
        }
    }
}

