using System.Collections.Generic;
using BookShop.Data.Models.Enums;
using BookShop.DataProcessor.ExportDto;

namespace BookShop.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var authors = context
                .Authors
                .Select(a => new
                {
                    AuthorName = $"{a.FirstName} {a.LastName}",
                    Books = a.AuthorsBooks
                        .Select(ab => ab.Book)
                        .Select(b => new
                        {
                            BookName = b.Name,
                            BookPrice = b.Price
                                .ToString("f2")
                        })
                        .OrderByDescending(b => decimal.Parse(b.BookPrice))
                        .ToArray()
                })
                .ToArray()
                .OrderByDescending(a => a.Books.Length)
                .ThenBy(a => a.AuthorName)
                .ToArray();

            return JsonConvert.SerializeObject(authors, Formatting.Indented);
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            var books = context
                .Books
                .Where(b => b.PublishedOn < date &&
                            b.Genre == Genre.Science)
                .OrderByDescending(b => b.Pages)
                .ThenByDescending(b => b.PublishedOn)
                .Take(10)
                .Select(b => new BookExportDto
                {
                    Name = b.Name,
                    Date = b.PublishedOn
                        .ToString("d",
                            CultureInfo.InvariantCulture),
                    Pages = b.Pages
                })
                .ToArray();

            return ToXml(books, "Books");
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