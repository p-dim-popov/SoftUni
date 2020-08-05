using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using BookShop.Data;
using BookShop.Data.Models;
using BookShop.Data.Models.Enums;
using BookShop.DataProcessor.ImportDto;
using Newtonsoft.Json;

namespace BookShop.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedBook
            = "Successfully imported book {0} for {1:F2}.";

        private const string SuccessfullyImportedAuthor
            = "Successfully imported author - {0} with {1} books.";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            var xmls = new XmlSerializer(typeof(BookImportBooks[]), new XmlRootAttribute("Books"));
            BookImportBooks[] xmlBooks;
            using (var strr = new StringReader(xmlString))
            {
                xmlBooks = xmls.Deserialize(strr) as BookImportBooks[];
            }

            var validBooks = new List<Book>();
            var sb = new StringBuilder();
            foreach (var xmlBook in xmlBooks)
            {
                if (!IsValid(xmlBook) || !DateTime.TryParseExact(xmlBook.PublishedOn, "MM/dd/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var publishedOn))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var book = new Book
                {
                    Name = xmlBook.Name,
                    Genre = (Genre) xmlBook.Genre,
                    Price = xmlBook.Price,
                    Pages = xmlBook.Pages,
                    PublishedOn = publishedOn
                };

                validBooks.Add(book);
                sb.AppendLine(string.Format(SuccessfullyImportedBook, xmlBook.Name, xmlBook.Price));
            }

            context.Books.AddRange(validBooks);
            context.SaveChanges();
            return sb.ToString();
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            var jsonAuthors = JsonConvert
                .DeserializeObject<AuthorImportAuthors[]>(
                    jsonString,
                    new JsonSerializerSettings() {NullValueHandling = NullValueHandling.Ignore}
                );

            var sb = new StringBuilder();

            foreach (var jsonAuthor in jsonAuthors)
            {
                if (!IsValid(jsonAuthor) ||
                    context.Authors.Any(a => a.Email == jsonAuthor.Email) || 
                    !CorrectAuthorBooks(jsonAuthor, context))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var author = new Author
                {
                    FirstName = jsonAuthor.FirstName,
                    LastName = jsonAuthor.LastName,
                    Email = jsonAuthor.Email,
                    Phone = jsonAuthor.Phone
                };

                context.Authors.Add(author);
                context.SaveChanges();

                var authorBooks = jsonAuthor.Books
                    .Select(book => new AuthorBook
                    {
                        AuthorId = author.Id,
                        BookId = book.Id.Value
                    })
                    .ToArray();

                context.AuthorsBooks.AddRange(authorBooks);
                context.SaveChanges();

                sb.AppendLine(string.Format(
                    SuccessfullyImportedAuthor,
                    $"{author.FirstName} {author.LastName}",
                    authorBooks.Length
                ));
            }

            return sb.ToString();
        }

        private static bool CorrectAuthorBooks(AuthorImportAuthors jsonAuthor, BookShopContext context)
        {
            jsonAuthor.Books = jsonAuthor.Books
                .Where(jsonBook => context.Books
                    .Any(dbBook => dbBook.Id == jsonBook.Id))
                .ToArray();

            return jsonAuthor.Books.Any();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}