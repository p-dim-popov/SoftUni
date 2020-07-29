using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.EntityFrameworkCore;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();
            // ctx.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //Import
            // Console.WriteLine(ImportUsers(context,
            //     File.ReadAllText($"../../../Datasets/users.xml")));
            // Console.WriteLine(ImportProducts(context,
            //     File.ReadAllText($"../../../Datasets/products.xml")));
            // Console.WriteLine(ImportCategories(context,
            //     File.ReadAllText($"../../../Datasets/categories.xml")));
            // Console.WriteLine(ImportCategoryProducts(context,
            //     File.ReadAllText($"../../../Datasets/categories-products.xml")));

            //Exports
            // Console.WriteLine(GetProductsInRange(context));
            // Console.WriteLine(GetSoldProducts(context));
            // Console.WriteLine(GetCategoriesByProductsCount(context));
            Console.WriteLine(GetUsersWithProducts(context));
        }

        #region ImportData

        //s1.
        public static string ImportUsers(ProductShopContext context, string inputXml)
            => ImportBulkDataFromXml<User>(context, inputXml, _ => true);

        //s2.
        public static string ImportProducts(ProductShopContext context, string inputXml)
            => ImportBulkDataFromXml<Product>(context, inputXml, _ => true);

        //s3.
        public static string ImportCategories(ProductShopContext context, string inputXml)
            => ImportBulkDataFromXml<Category>(context, inputXml, _ => true);

        //s4.
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
            => ImportBulkDataFromXml<CategoryProduct>(context, inputXml,
                x => context.Categories.Any(c => c.Id == x.CategoryId) &&
                     context.Products.Any(p => p.Id == x.ProductId));

        public static string ImportBulkDataFromXml<T>(ProductShopContext context,
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

        //s5.
        public static string GetProductsInRange(ProductShopContext context)
        {
            var data = context.Products
                .OrderBy(p => p.Price)
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new ProductsInRange
                {
                    Name = p.Name,
                    Price = p.Price,
                    Buyer = $"{p.Buyer.FirstName} {p.Buyer.LastName}"
                })
                .Take(10)
                .ToArray();

            var serializer = new XmlSerializer(
                typeof(ProductsInRange[]),
                new XmlRootAttribute("Products")
            );

            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter,
                new XmlWriterSettings {Indent = true}))
            {
                var xmlns = new XmlSerializerNamespaces();
                xmlns.Add("", "");
                serializer.Serialize(xmlWriter, data, xmlns);
                return stringWriter.ToString();
            }
        }

        //s6.
        public static string GetSoldProducts(ProductShopContext context)
        {
            var data = context.Users
                .Include(x => x.ProductsSold)
                .Where(u => u.ProductsSold.Any())
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new UserSoldProducts
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold
                        .Select(p => new SoldProduct
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                        .ToArray()
                })
                .Take(5)
                .ToArray();

            var serializer = new XmlSerializer(
                typeof(UserSoldProducts[]),
                new XmlRootAttribute("Users"));
            using (var sw = new StringWriter())
            using (var xmlw = XmlWriter.Create(sw, new XmlWriterSettings() {Indent = true}))
            {
                var xmlns = new XmlSerializerNamespaces();
                xmlns.Add("", "");
                serializer.Serialize(xmlw, data, xmlns);
                return sw.ToString();
            }
        }

        //s7.
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var data = context.Categories
                .Select(x => new CategoryByProductsCount
                {
                    Name = x.Name,
                    Count = x.CategoryProducts.Count,
                    AveragePrice = x.CategoryProducts
                        .Average(y => y.Product.Price),
                    TotalRevenue = x.CategoryProducts
                        .Sum(y => y.Product.Price)
                })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.TotalRevenue)
                .ToArray();

            var xmls = new XmlSerializer(typeof(CategoryByProductsCount[]),
                new XmlRootAttribute("Categories"));

            using (var sw = new StringWriter())
            {
                using (var xmlw = XmlWriter.Create(sw, new XmlWriterSettings() {Indent = true}))
                {
                    var xmlns = new XmlSerializerNamespaces();
                    xmlns.Add("", "");
                    xmls.Serialize(xmlw, data, xmlns);
                    return sw.ToString();
                }
            }
        }

        //s8.
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var data = context.Users
                .Include(u => u.ProductsSold)
                .Where(u => u.ProductsSold.Any())
                .OrderByDescending(u => u.ProductsSold.Count)
                .Take(10)
                .ToArray()
                .Select(u => new UserUsersAndProducts
                {
                    FirstName = u.FirstName, LastName = u.LastName, Age = u.Age,
                    SoldProducts = new SoldProductsUsersAndProducts
                    {
                        Count = u.ProductsSold.Count,
                        Products = u.ProductsSold
                            .Select(p => new SoldProductUsersAndProducts
                            {
                                Name = p.Name,
                                Price = p.Price
                            })
                            .OrderByDescending(p => p.Price)
                            // .Take(10)
                            .ToArray()
                    }
                })
                .ToArray();

            var dataWithCountProperty = new UsersWithCountUsersAndProducts
            {
                Count = context.Users.Count(u => u.ProductsSold.Any()),
                Users = data
            };

            var xmls = new XmlSerializer(dataWithCountProperty.GetType(),
                new XmlRootAttribute("Users"));
            var sw = new StringWriter() { Encoding = {}};
            var xmlw = XmlWriter.Create(sw, new XmlWriterSettings()
            {
                Indent = true, 
                Encoding = Encoding.UTF8
            });
            var xmlns = new XmlSerializerNamespaces();
            xmlns.Add("", "");
            xmls.Serialize(xmlw, dataWithCountProperty, xmlns);
            string xml = sw.ToString();
            xmlw.Close();
            sw.Close();
            return xml;
        }
    }
}