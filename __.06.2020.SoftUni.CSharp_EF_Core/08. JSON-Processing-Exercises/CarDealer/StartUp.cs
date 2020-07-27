using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static JsonSerializerSettings JsonSerializingSettings { get; } = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            // ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            // context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //Import
            // Console.WriteLine(ImportFromJson(context, ImportSuppliers));
            // Console.WriteLine(ImportFromJson(context, ImportParts));
            // Console.WriteLine(ImportFromJson(context, ImportCars));
            // Console.WriteLine(ImportFromJson(context, ImportCustomers));
            // Console.WriteLine(ImportFromJson(context, ImportSales));

            //Export
            // Console.WriteLine(GetOrderedCustomers(context));
            // Console.WriteLine(GetCarsFromMakeToyota(context));
            // Console.WriteLine(GetLocalSuppliers(context));
            // Console.WriteLine(GetCarsWithTheirListOfParts(context));
            // Console.WriteLine(GetTotalSalesByCustomer(context));
            Console.WriteLine(GetSalesWithAppliedDiscount(context));
        }


        #region Imports

        //s10.
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
            => DeserializeAddEntities<Supplier>(context, inputJson, user => { return true; });

        //s11.
        public static string ImportParts(CarDealerContext context, string inputJson)
            => DeserializeAddEntities<Part>(context, inputJson, product =>
            {
                if (product.Id < 0) return false;
                if (!context.Suppliers.Any(s => s.Id == product.SupplierId)) return false;
                return true;
            });

        //s12.
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var jsonCars = JsonConvert
                .DeserializeObject<Dictionary<string, object>[]>(inputJson)
                .ToList();

            var cars = new List<Car>(jsonCars.Count);
            var partCars = new List<PartCar>();

            foreach (var jsonCar in jsonCars)
            {
                var car = new Car()
                {
                    Make = jsonCar["make"] as string,
                    Model = jsonCar["model"] as string,
                    TravelledDistance = jsonCar["travelledDistance"] as long? ?? 0L
                };

                cars.Add(car);
                partCars.AddRange(
                    (jsonCar["partsId"] as JArray ?? new JArray())
                    .Distinct()
                    .Select(x => new PartCar()
                    {
                        PartId = (int) x,
                        Car = car
                    })
                    .ToList()
                );
            }

            context.Cars.AddRange(cars);
            context.PartCars.AddRange(partCars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count()}.";
        }

        //s13.
        public static string ImportCustomers(CarDealerContext context, string inputJson)
            => DeserializeAddEntities<Customer>(context, inputJson, categoryProducts => { return true; });

        public static string ImportSales(CarDealerContext context, string inputJson)
            => DeserializeAddEntities<Sale>(context, inputJson, customer => true);

        #region ImportDataHelpersRegion

        public static string DeserializeAddEntities<T>(
            CarDealerContext context,
            string inputJson,
            Predicate<T> predicate
        )
            where T : class, new()
        {
            var entities = JsonConvert
                .DeserializeObject<T[]>(inputJson)
                .Where(x => predicate(x))
                .ToArray();

            context.AddRange(entities);
            context.SaveChanges();
            return $"Successfully imported {entities.Count()}.";
        }

        public static string ImportFromJson(
            CarDealerContext context,
            Func<CarDealerContext, string, string> func)
        {
            var inputJsonFile = string.Join("", func.Method.Name
                    .Replace("Import", "")
                    .Select(x => x.ToString())
                    .Select((x, i) => x.ToLower() != x && i != 0 ? $"-{x}" : x))
                .ToLower() + ".json";

            var inputJson = File.ReadAllText($"../../../Datasets/{inputJsonFile}");
            return func(context, inputJson);
        }

        #endregion

        #endregion

        //s14.
        public static string GetOrderedCustomers(CarDealerContext context)
            => JsonConvert.SerializeObject(
                context.Customers
                    .OrderBy(c => c.BirthDate)
                    .ThenBy(c => c.IsYoungDriver)
                    .Select(c => new
                    {
                        c.Name,
                        BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                        c.IsYoungDriver
                    })
                ,
                JsonSerializingSettings);

        //s15.
        public static string GetCarsFromMakeToyota(CarDealerContext context)
            => JsonConvert.SerializeObject(
                context.Cars
                    .Where(c => c.Make.ToLower().Equals("toyota"))
                    .OrderBy(c => c.Model)
                    .ThenByDescending(c => c.TravelledDistance)
                    .Select(c => new
                    {
                        c.Id,
                        c.Make,
                        c.Model,
                        c.TravelledDistance
                    })
                ,
                JsonSerializingSettings
            );

        //s16.
        public static string GetLocalSuppliers(CarDealerContext context)
            => JsonConvert.SerializeObject(
                context.Suppliers
                    .Where(s => !s.IsImporter)
                    .Select(s => new
                    {
                        s.Id,
                        s.Name,
                        PartsCount = s.Parts.Count
                    })
                ,
                JsonSerializingSettings
            );
        
        //s17.
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
            => JsonConvert.SerializeObject(
                context.Cars
                    .Select(c => new
                    {
                        car = new
                        {
                            c.Make,
                            c.Model,
                            c.TravelledDistance
                        },
                        
                        parts = c.PartCars
                            .Select(pc => new
                            {
                                pc.Part.Name,
                                Price = pc.Part.Price.ToString("f2")
                            })
                    })
                ,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented
                });
        
        //s18.
        public static string GetTotalSalesByCustomer(CarDealerContext context)
            => JsonConvert.SerializeObject(
                context.Customers
                    .Where(c => c.Sales.Any())
                    .Select(c => new
                    {
                        FullName = c.Name,
                        BoughtCars = c.Sales.Count,
                        SpentMoney = c.Sales
                            .Sum(s => s.Car.PartCars
                                .Sum(pc => pc.Part.Price))
                    })
                    .OrderByDescending(x => x.SpentMoney)
                ,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            );
        
        //s19.
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
            => JsonConvert.SerializeObject(
                context.Sales
                    .Take(10)
                    .Select(sale => new
                    {
                        car = new
                        {
                            sale.Car.Make,
                            sale.Car.Model,
                            sale.Car.TravelledDistance
                        },
                        
                        customerName = sale.Customer.Name,
                        Discount = sale.Discount.ToString("f2"),
                        price = sale.Car.PartCars
                            .Sum(x => x.Part.Price)
                            .ToString("f2"),
                        priceWithDiscount = 
                            (sale.Car.PartCars.Sum(x => x.Part.Price) * (1 - sale.Discount / 100m))
                            .ToString("f2")
                    })
                ,
                new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented
                }
                );
    }
}