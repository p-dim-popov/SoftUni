using System.Xml;
using BookShop.Data.Models;
using BookShop.Data.Models.Enums;
using BookShop.DataProcessor.ImportDto;

namespace BookShop.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedBook
            = "Successfully imported book {0} for {1:F2}.";

        private const string SuccessfullyImportedAuthor
            = "Successfully imported author - {0} with {1} books.";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            var xmlBooks =
                FromXmlTo<BookImportDto[]>(xmlString, "Books");

            var sb = new StringBuilder();
            var validBooks = new List<Book>();
            foreach (var xmlBook in xmlBooks)
            {
                if (!IsValid(xmlBook))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isBookPublishedOnValid =
                    DateTime.TryParseExact(xmlBook.PublishedOn,
                        "MM/dd/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var bookPublishedOnValue);
                if (!isBookPublishedOnValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var book = new Book();
                book.Name = xmlBook.Name;
                book.Genre = (Genre) xmlBook.Genre;
                book.Price = xmlBook.Price;
                book.Pages = xmlBook.Pages;
                book.PublishedOn = bookPublishedOnValue;

                validBooks.Add(book);
                sb.AppendLine(string.Format(
                    SuccessfullyImportedBook,
                    book.Name,
                    book.Price));
            }

            context.Books.AddRange(validBooks);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            var jsonAuthors = JsonConvert
                .DeserializeObject<AuthorImportDto[]>(jsonString);

            var sb = new StringBuilder();
            var validAuthors = new List<Author>();
            foreach (var jsonAuthor in jsonAuthors)
            {
                if (!IsValid(jsonAuthor))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isEmailExising = validAuthors
                    .Any(a => a.Email == jsonAuthor.Email);
                if (isEmailExising)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var author = new Author();
                author.FirstName = jsonAuthor.FirstName;
                author.LastName = jsonAuthor.LastName;
                author.Phone = jsonAuthor.Phone;
                author.Email = jsonAuthor.Email;

                foreach (var jsonBook in jsonAuthor.Books)
                {
                    if (!IsValid(jsonBook))
                        continue;

                    bool isBookExistent =
                        context.Books
                            .Any(b => b.Id == jsonBook.Id);
                    if (!isBookExistent)
                        continue;
                    
                    var authorBook = new AuthorBook();
                    authorBook.BookId = jsonBook.Id.Value;
                    
                    author.AuthorsBooks.Add(authorBook);
                }

                if (!author.AuthorsBooks.Any())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                
                validAuthors.Add(author);
                sb.AppendLine(string.Format(
                    SuccessfullyImportedAuthor,
                    $"{author.FirstName} {author.LastName}",
                    author.AuthorsBooks.Count));
            }

            context.Authors.AddRange(validAuthors);
            context.SaveChanges();

            return sb.ToString();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
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