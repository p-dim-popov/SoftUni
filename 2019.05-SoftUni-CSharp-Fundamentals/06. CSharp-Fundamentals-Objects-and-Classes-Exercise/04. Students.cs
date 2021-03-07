using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        var students = new List<Student>();

        for (int i = 0; i < n; i++)
        {
            string[] tokens = Console.ReadLine()
                .Split(" ")
                .ToArray();

            string firstName = tokens[0];
            string secondName = tokens[1];
            float grade = float.Parse(tokens[2]);

            students.Add(new Student(firstName, secondName, grade));
        }

        var studentsOrderedByGrade = students.OrderBy(g => g.Grade).ToList();
        studentsOrderedByGrade.Reverse();

        foreach (var student in studentsOrderedByGrade)
        {
            Console.WriteLine(student);
        }

    }
}
class Student
{
    public Student(string firstName, string secondName, float grade)
    {
        this.FirtstName = firstName;
        this.SecondName = secondName;
        this.Grade = grade;
    }
    public string FirtstName { get; set; }
    public string SecondName { get; set; }
    public float Grade { get; set; }

    public override string ToString()
    {
        return $"{this.FirtstName} {this.SecondName}: {this.Grade:f2}";
    }
}