using System.Collections.Generic;
using BookShop.Data.Models;
using BookShop.Data.Models.Enums;
using BookShop.DataProcessor.ExportDto;
using Microsoft.EntityFrameworkCore;

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
            var data = context.Authors
                .Select(a => new
                {
                    AuthorName = $"{a.FirstName} {a.LastName}",
                    Books = a.AuthorsBooks
                        .Select(ab => ab.Book)
                        .OrderByDescending(b => b.Price)
                        .Select(b => new
                        {
                            BookName = b.Name,
                            BookPrice = b.Price.ToString("f2")
                        })
                        .ToArray()
                })
                .ToArray()
                .OrderByDescending(x => x.Books.Count())
                .ThenBy(x => x.AuthorName)
                .ToArray();

            var json = JsonConvert.SerializeObject(data, Formatting.Indented);

            return json;
        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            var data = context.Books
                .OrderByDescending(b => b.Pages)
                .ThenByDescending(b => b.PublishedOn)
                .Where(b => b.PublishedOn < date && b.Genre == Genre.Science)
                .Select(b => new BookExportOldestBooks
                {
                    Pages = b.Pages,
                    Name = b.Name,
                    Date = b.PublishedOn.ToString("d", CultureInfo.InvariantCulture)
                })
                .Take(10)
                .ToArray();

            return ToXml(data, "Books");
        }

        private static string ToXml<T>(ICollection<T> data, string rootAttributeName = null)
            where T : new()
        {
            XmlSerializer xmls;
            if (rootAttributeName is null)
                xmls = new XmlSerializer(data.GetType());
            else
                xmls = new XmlSerializer(data.GetType(),
                    new XmlRootAttribute(rootAttributeName));
            using (var sw = new StringWriter())
            using (var xmlw = XmlWriter.Create(sw,
                new XmlWriterSettings {Indent = true}))
            {
                var xmlns = new XmlSerializerNamespaces();
                xmlns.Add("", "");
                xmls.Serialize(xmlw, data, xmlns);
                var xml = sw.ToString();
                return xml;
            }
        }
    }
}