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
        static void Main()
        {
            string line = Console.ReadLine();
            var students = new List<Student>();
            while (line != "end")
            {
                string[] tokens = line.Split().ToArray();

                string firstName = tokens[0];
                string lastName = tokens[1];
                int age = int.Parse(tokens[2]);
                string city = tokens[3];

                int matchIndex;

                if (int.TryParse(CheckIfExists(students, firstName, lastName), out matchIndex))
                {
                    students[matchIndex].FirstName = firstName;
                    students[matchIndex].LastName = lastName;
                    students[matchIndex].Age = age;
                    students[matchIndex].City = city;

                }
                else
                {
                    students.Add(new Student()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Age = age,
                        City = city
                    });
                }

                /*
                if (student == null)
                {
                    students.Add(new Student()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Age = age,
                        City = city
                    });
                }
                else
                {
                    student.FirstName = firstName;
                    student.LastName = lastName;
                    student.Age = age;
                    student.City = city;
                }*/

                line = Console.ReadLine();
            }

            string filterCity = Console.ReadLine();
            var filteredByCity = students
                .Where(x => x.City == filterCity)
                .ToList();
            foreach (var student in filteredByCity)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName} is {student.Age} years old.");
            }
        }

        static string CheckIfExists(List<Student> students, string firstName, string lastName)
        {
            int i = 0;
            foreach(var student in students)
            {
                if (firstName == student.FirstName && lastName == student.LastName)
                {
                    return $"{i}";
                }
                i++;
            }
            return "no matches";
        }
    }

    class Student
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string City { get; set; }

    }
}