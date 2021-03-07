using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Cinema.Data.Models;
using Cinema.DataProcessor.ExportDto;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Cinema.DataProcessor
{
    using System;
    using Data;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var movies = context.Movies
                .Include(m => m.Projections)
                .Include("Projections.Tickets")
                .Include("Projections.Tickets.Customer")
                .ToArray()
                .Where(m => m.Rating >= rating)
                .Where(m => m.Projections
                    .Any(p => p.Tickets
                        .Any()))
                .Select(m => new
                {
                    MovieName = m.Title,
                    Rating = m.Rating.ToString("f2"),
                    TotalIncomes = m.Projections
                        .Sum(p => p.Tickets
                            .Sum(t => t.Price))
                        .ToString("f2"),
                    Customers = m.Projections
                        .Select(p => p.Tickets
                            .Select(t => t.Customer))
                        .Aggregate(new List<Customer>(),
                            (list, customers) => list
                                .Concat(customers)
                                .ToList())
                        .Select(c => new
                        {
                            c.FirstName,
                            c.LastName,
                            Balance = c.Balance.ToString("f2")
                        })
                        .OrderByDescending(c => c.Balance)
                        .ThenBy(c => c.FirstName)
                        .ThenBy(c => c.LastName)
                        .ToArray()
                })
                .OrderByDescending(m => decimal.Parse(m.Rating))
                .ThenByDescending(m => decimal.Parse(m.TotalIncomes))
                .Take(10)
                .ToArray();

            return JsonConvert.SerializeObject(movies, Formatting.Indented);
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            var customers = context.Customers
                .Include(c => c.Tickets)
                .ToArray()
                .Where(c => c.Age >= age)
                .Select(c => new CustomerExportDto
                {
                    FirstName = c.FirstName, LastName = c.LastName,
                    SpentMoney = c.Tickets
                        .Sum(t => t.Price)
                        .ToString("f2"),
                    SpentTime = new TimeSpan(
                            c.Tickets
                                .Sum(t => t
                                    .Projection
                                    .Movie
                                    .Duration
                                    .Ticks))
                        .ToString(@"hh\:mm\:ss")
                })
                .OrderByDescending(c => decimal.Parse(c.SpentMoney))
                .Take(10)
                .ToArray();

            return ToXml(customers, "Customers");
        }


        private static T FromXmlTo<T>(string xmlString, string rootElement = null)
            where T : class
        {
            var xmls = string.IsNullOrWhiteSpace(rootElement)
                ? new XmlSerializer(typeof(T))
                : new XmlSerializer(typeof(T), new XmlRootAttribute(rootElement));

            using (var strr = new StringReader(xmlString))
                return xmls.Deserialize(strr) as T;
        }


        private static string ToXml<T>(ICollection<T> data, string rootElement = null)
            where T : new()
        {
            var xmls = string.IsNullOrWhiteSpace(rootElement)
                ? new XmlSerializer(data.GetType())
                : new XmlSerializer(data.GetType(), new XmlRootAttribute(rootElement));
            using (var sw = new StringWriter())
            using (var xmlw = XmlWriter.Create(sw,
                new XmlWriterSettings {Indent = true}))
            {
                var xmlns = new XmlSerializerNamespaces();
                xmlns.Add("", "");
                xmls.Serialize(xmlw, data, xmlns);
                return sw.ToString();
            }
        }
    }
}