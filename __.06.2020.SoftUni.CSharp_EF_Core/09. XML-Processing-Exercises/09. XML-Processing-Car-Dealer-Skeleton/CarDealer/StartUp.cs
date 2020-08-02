using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using CarDealer.Data;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CarDealer
{
    public static class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            // context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //Imports
            // Console.WriteLine(ImportSuppliers(context,
            //     File.ReadAllText("../../../Datasets/suppliers.xml")));
            // Console.WriteLine(ImportParts(context,
            //     File.ReadAllText("../../../Datasets/parts.xml")));
            // Console.WriteLine(ImportCars(context,
            //     File.ReadAllText("../../../Datasets/cars.xml")));
            // Console.WriteLine(ImportCustomers(context,
            //     File.ReadAllText("../../../Datasets/customers.xml")));
            // Console.WriteLine(ImportSales(context,
            //     File.ReadAllText("../../../Datasets/sales.xml")));

            //Exports
            // Console.WriteLine(GetCarsWithDistance(context));
            // Console.WriteLine(GetCarsFromMakeBmw(context));
            // Console.WriteLine(GetLocalSuppliers(context));
            // Console.WriteLine(GetCarsWithTheirListOfParts(context));
            // Console.WriteLine(GetTotalSalesByCustomer(context));
            Console.WriteLine(GetSalesWithAppliedDiscount(context));
        }

        #region Imports

        //s9.
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
            => ImportBulkDataFromXml<Supplier>(context, inputXml, _ => true);

        //s10.
        public static string ImportParts(CarDealerContext context, string inputXml)
            => ImportBulkDataFromXml<Part>(context, inputXml,
                part =>
                {
                    if (!context.Suppliers.Any(s => s.Id == part.SupplierId)) return false;
                    return true;
                });

        //s11.
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(CarsImportCars));
            var dtoCars = serializer.Deserialize(new StringReader(inputXml)) as CarsImportCars;

            foreach (var dtoCar in dtoCars.Collection)
            {
                var car = new Car
                {
                    Make = dtoCar.Make,
                    Model = dtoCar.Model,
                    TravelledDistance = dtoCar.TraveledDistance
                };
                context.Cars.Add(car);
                context.SaveChanges();

                var partCars = dtoCar.Parts.PartIds
                    .GroupBy(x => x.Id)
                    .Select(x => x.First())
                    .Where(x => context.Parts.Any(p => p.Id == x.Id))
                    .Select(partId => new PartCar()
                    {
                        Car = car,
                        PartId = partId.Id
                    })
                    .ToList();

                context.PartCars.AddRange(partCars);
                context.SaveChanges();
            }

            return $"Successfully imported {dtoCars.Collection.Length}";
        }

        //s12.
        public static string ImportCustomers(CarDealerContext context, string inputXml)
            => ImportBulkDataFromXml<Customer>(context, inputXml, _ => true);

        //s13.
        public static string ImportSales(CarDealerContext context, string inputXml)
            => ImportBulkDataFromXml<Sale>(context, inputXml, sale =>
            {
                if (!context.Cars.Any(c => c.Id == sale.CarId)) return false;
                return true;
            });

        public static string ImportBulkDataFromXml<T>(CarDealerContext context,
            string inputXml,
            Predicate<T> predicate,
            string rootElement = null) where T : class, new()
        {
            var tableName = context
                .GetType()
                .GetProperties()
                .FirstOrDefault(x
                    => x
                        .PropertyType
                        .GenericTypeArguments
                        .FirstOrDefault(gta => gta.Name == typeof(T).Name) != null)
                ?.Name;
            var serializer = new XmlSerializer(typeof(T[]), new XmlRootAttribute(rootElement ?? tableName));
            var entities = (serializer.Deserialize(new StringReader(inputXml)) as T[])
                .Where(x => predicate(x))
                .ToArray();
            var table = context.GetType().GetProperty(tableName).GetValue(context) as DbSet<T>;
            table.AddRange(entities);
            context.SaveChanges();
            return $"Successfully imported {entities.Length}";
        }

        #endregion

        #region Exports

        //s14.
        public static string GetCarsWithDistance(CarDealerContext context)
            => context.Cars
                .Where(c => c.TravelledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .Select(x => new CarCarsWithDistance
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .ToList()
                .ToXml("cars");

        //s15.
        public static string GetCarsFromMakeBmw(CarDealerContext context)
            => context.Cars
                .Where(c => c.Make == "BMW")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new CarCarsFromMakeBmw
                {
                    Id = c.Id,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToArray()
                .ToXml("cars");

        //s16.
        public static string GetLocalSuppliers(CarDealerContext context)
            => context.Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new SupplierLocalSuppliers
                {
                    Id = s.Id, Name = s.Name, PartsCount = s.Parts.Count
                })
                .ToArray()
                .ToXml("suppliers");

        //s17.
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
            => context.Cars
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .Select(c => new CarCarsWithTheirParts
                {
                    Make = c.Make, Model = c.Model, TravelledDistance = c.TravelledDistance,
                    Parts = c.PartCars
                        .Select(pc => new PartCarsWithTheirParts
                        {
                            Name = pc.Part.Name, Price = pc.Part.Price
                        })
                        .OrderByDescending(p => p.Price)
                        .ToArray()
                })
                .ToArray()
                .ToXml("cars");

        //s18.
        public static string GetTotalSalesByCustomer(CarDealerContext context)
            // => context.Customers
            //         .Where(customer => customer.Sales.Any())
            //         .Select(cust => new CustomerTotalSalesByCustomer
            //         {
            //             FullName = cust.Name, 
            //             BoughtCars = cust.Sales.Count,
            //             SpentMoney = cust.Sales
            //                 .Select(sale => sale.Car.PartCars
            //                     .Sum(pc => pc.Part.Price))
            //                 .ToArray()
            //                 .Sum()
            //         })
            //         .ToArray()
            //         .OrderByDescending(x => x.SpentMoney)
            //         .ToArray()
            //         .ToXml("customers");
            //// I have no idea why the judging system won't accept my upper solution
            //// The exception is:
            //// System.InvalidCastException : Unable to cast object of type 'System.Linq.Expressions.NewExpression'
            //// to type 'System.Linq.Expressions.MethodCallExpression'.
            //// at Microsoft.EntityFrameworkCore.InMemory.Query.Internal.InMemoryQueryExpression.AddSubqueryProjection(
            //// ShapedQueryExpression shapedQueryExpression, Expression& innerShaper)
            => context.Customers
                .Include(c => c.Sales)
                .Include("Sales.Car.PartCars.Part")
                .Where(c => c.Sales.Any()).ToArray()
                .ToArray()
                .Select(cust => new CustomerTotalSalesByCustomer
                {
                    FullName = cust.Name,
                    BoughtCars = cust.Sales.Count,
                    SpentMoney = cust.Sales
                        .Sum(sale => sale.Car.PartCars
                            .Sum(pc => pc.Part.Price))
                })
                .OrderByDescending(x => x.SpentMoney)
                .ToArray()
                .ToXml("customers");

        //s19.
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
            => context.Sales
                .Select(sale => new SaleSalesWithAppliedDiscount
                {
                    Car = new CarSalesWithAppliedDiscount
                    {
                        Make = sale.Car.Make,
                        Model = sale.Car.Model,
                        TravelledDistance = sale.Car.TravelledDistance
                    },
                    Discount = sale.Discount.ToString("0.####"),
                    CustomerName = sale.Customer.Name,
                    Price = sale.Car.PartCars
                        .Sum(x => x.Part.Price)
                        .ToString("0.####"),
                    PriceWithDiscount = (sale.Car.PartCars
                            .Sum(x => x.Part.Price) * (1M - sale.Discount / 100M))
                        .ToString("0.####")
                })
                .ToArray()
                .ToXml("sales");

        public static string ToXml<T>(this ICollection<T> data, string rootAttributeName = null)
        {
            XmlSerializer xmls;
            if (rootAttributeName is null)
                xmls = new XmlSerializer(data.GetType());
            else
                xmls = new XmlSerializer(data.GetType(),
                    new XmlRootAttribute(rootAttributeName));
            using var sw = new StringWriter();
            using var xmlw = XmlWriter.Create(sw,
                new XmlWriterSettings {Indent = true});
            var xmlns = new XmlSerializerNamespaces();
            xmlns.Add("", "");
            xmls.Serialize(xmlw, data, xmlns);
            var xml = sw.ToString();
            return xml;
        }

        #endregion
    }
}