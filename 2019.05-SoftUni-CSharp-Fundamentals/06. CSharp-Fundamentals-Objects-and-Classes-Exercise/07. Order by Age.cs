using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class Program
{
    static void Main()
    {
        var listOfPeople = new List<People>();
        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            string[] token = input.Split(" ");
            string name = token[0];
            string id = token[1];
            string age = token[2];

            listOfPeople.Add(new People(name, id, age));
        }
        var peopleOrderedByAge = listOfPeople.OrderBy(p => p.Age);
        foreach(var person in peopleOrderedByAge)
        {
            Console.WriteLine(person);
        }
    }
}
class People
{
    public People(string name, string id, string age)
    {
        this.Name = name;
        this.Id = id;
        this.Age = int.Parse(age);
    }
    public string Name { get; set; }
    public string Id { get; set; }
    public int Age { get; set; }

    public override string ToString()
    {
        return $"{this.Name} with ID: {this.Id} is {this.Age} years old.";
    }
}