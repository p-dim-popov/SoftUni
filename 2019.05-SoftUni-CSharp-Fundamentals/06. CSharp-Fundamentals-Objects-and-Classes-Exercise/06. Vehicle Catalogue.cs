using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class Program
{
    static void Main()
    {
        var catalogue = new List<Vehicles>();
        string input;

        while((input = Console.ReadLine()) != "End")
        {
            string[] token = input.Split(" ");
            
            string type = token[0];
            string model = token[1];
            string color = token[2];
            string horsepower = token[3];

            catalogue.Add(new Vehicles(type, model, color, horsepower));
        }

        while ((input = Console.ReadLine()) != "Close the Catalogue")
        {
            var vehicles = catalogue
                .Where(x => x.Model == input);
            foreach(var vehicle in vehicles)
            {
                vehicle.PrintInfo();
            }
        }

        var cars = catalogue
            .Where(x => x.Type == "Car")
            .ToList();
        var trucks = catalogue
            .Where(x => x.Type == "Truck")
            .ToList();

        double averageHpCars = 0;
        double averageHpTrucks = 0;

        foreach(var car in cars)
        {
            averageHpCars += car.Horsepower;
        }
        foreach(var truck in trucks)
        {
            averageHpTrucks += truck.Horsepower;
        }

        if (cars.Count != 0)
        {
            averageHpCars /= cars.Count;
        }
        if (trucks.Count != 0)
        {
            averageHpTrucks /= trucks.Count;
        }

        Console.WriteLine($"Cars have average horsepower of: {averageHpCars:f2}.");
        Console.WriteLine($"Trucks have average horsepower of: {averageHpTrucks:f2}.");

    }

}
class Vehicles
{
    public Vehicles(string type, string model, string color, string horsepower)
    {
        if (type == "car")
        {
            this.Type = "Car";
        }
        else if (type == "truck")
        {
            this.Type = "Truck";
        }
        this.Model = model;
        this.Color = color;
        this.Horsepower = int.Parse(horsepower);
    }

    public string Type { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public int Horsepower { get; set; }

    public void PrintInfo()
    {
        Console.WriteLine($"Type: {this.Type}");
        Console.WriteLine($"Model: {this.Model}");
        Console.WriteLine($"Color: {this.Color}");
        Console.WriteLine($"Horsepower: {this.Horsepower}");
    }
}