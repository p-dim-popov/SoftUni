using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Initializer;
using BookShop.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    using Data;

    public static class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);
            // db.Database.EnsureCreated();
            // Console.WriteLine(GetBooksByAgeRestriction(db, "miNor"));
            // Console.WriteLine(GetGoldenBooks(db));
            // Console.WriteLine(GetBooksByPrice(db));
            // Console.WriteLine(GetBooksNotReleasedIn(year: 2000, context: db));
            // Console.WriteLine(GetBooksByCategory(db, "horror mystery drama"));
            // Console.WriteLine(GetBooksReleasedBefore(db, "30-12-1989"));
            // Console.WriteLine(GetAuthorNamesEndingIn(db, "dy"));
            // Console.WriteLine(GetBookTitlesContaining(db, "sK"));
            // Console.WriteLine(GetBooksByAuthor(db, "po"));
            // Console.WriteLine(CountBooks(db, 12));
            // Console.WriteLine(CountCopiesByAuthor(db));
            // Console.WriteLine(GetTotalProfitByCategory(db));
            // Console.WriteLine(GetMostRecentBooks(db));
            //
            Console.WriteLine(RemoveBooks(db));
        }

        //15.Remove Books
        public static int RemoveBooks(BookShopContext context)
        {
            var booksToDelete = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();
            context.RemoveRange(booksToDelete);
            context.SaveChanges();
            return booksToDelete.Count();
        }

        //14.Increase Prices
        public static void IncreasePrices(BookShopContext context)
            => context.Books
                .Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year < 2010)
                .ForEachAsync(b => b.Price += 5);
        // .ContinueWith(_ => context.SaveChanges());

        //13.Most Recent Books
        public static string GetMostRecentBooks(BookShopContext context)
            => context.Categories
                .OrderBy(c => c.Name)
                .Select(x => @$"--{x.Name}{Environment.NewLine}{
                        x.CategoryBooks
                            .Select(bc => bc.Book)
                            .Where(b => b.ReleaseDate.HasValue)
                            .OrderByDescending(b => b.ReleaseDate.Value)
                            .Take(3)
                            .Select(b => $"{b.Title} ({b.ReleaseDate.Value.Year})")
                            .StringJoin(Environment.NewLine)
                    }")
                .StringJoin(Environment.NewLine);

        //12.Profit by Category
        public static string GetTotalProfitByCategory(BookShopContext context)
            => context.Categories
                .Select(c => new
                {
                    c.Name,
                    Profit = c.CategoryBooks.Select(cb => cb.Book.Copies * cb.Book.Price).Sum()
                })
                .OrderByDescending(x => x.Profit)
                .ThenBy(x => x.Name)
                .Select(x => $"{x.Name} ${x.Profit:f2}")
                .StringJoin(Environment.NewLine);

        //11.Total Book Copies
        public static string CountCopiesByAuthor(BookShopContext context)
            => context.Authors
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName,
                    TotalBookCopies = a.Books.Select(b => b.Copies).Sum()
                })
                .OrderByDescending(a => a.TotalBookCopies)
                .Select(a => $"{a.FirstName} {a.LastName} - {a.TotalBookCopies}")
                .StringJoin(Environment.NewLine);

        //10.Count Books
        public static int CountBooks(BookShopContext context, int lengthCheck)
            => context.Books
                .Count(b => b.Title.Length > lengthCheck);

        //9.Book Search by Author
        public static string GetBooksByAuthor(BookShopContext context, string input)
            => context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})")
                .StringJoin(Environment.NewLine);

        //8. Book Search
        public static string GetBookTitlesContaining(BookShopContext context, string input)
            => context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .StringJoin(Environment.NewLine);

        //7.Author Search
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
            => context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .Select(a => $"{a.FirstName} {a.LastName}")
                .StringJoin(Environment.NewLine);

        //6.Released Before Date
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
            => context.Books
                .Where(b => b.ReleaseDate < DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture))
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => $"{b.Title} - {b.EditionType.ToString()} - ${b.Price:f2}")
                .StringJoin(Environment.NewLine);

        //5.Book Titles by Category
        public static string GetBooksByCategory(BookShopContext context, string input)
            => context.BooksCategories
                .Where(b => input
                    .ToLowerInvariant()
                    .Split(
                        new[]
                        {
                            ' ', '\t', '\r', '\n'
                        },
                        StringSplitOptions.RemoveEmptyEntries
                    )
                    .Contains(b.Category.Name.ToLower()))
                .Select(x => x.Book.Title)
                .OrderBy(x => x)
                .StringJoin(Environment.NewLine);

        //4.Not Released In
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
            => context.Books
                .Where(b => b.ReleaseDate != null && b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .StringJoin(Environment.NewLine);

        //3.Books by Price
        public static string GetBooksByPrice(BookShopContext context)
            => context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => $"{b.Title} - ${b.Price:F2}")
                .StringJoin(Environment.NewLine);

        //2.Golden Books
        public static string GetGoldenBooks(BookShopContext context)
            => context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .StringJoin(Environment.NewLine);

        //1.Age Restriction
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
            => context.Books
                .Where(
                    b
                        => b.AgeRestriction == Enum.Parse<AgeRestriction>(command, true))
                .Select(b => b.Title)
                .OrderByDescending(t => t)
                .StringJoin(Environment.NewLine);


        public static string StringJoin<T>(this IEnumerable<T> coll, string sep)
        {
            return string.Join(sep, coll);
        }
    }
}