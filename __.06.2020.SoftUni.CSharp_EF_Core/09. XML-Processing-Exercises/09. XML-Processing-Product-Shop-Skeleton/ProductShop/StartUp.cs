using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            var ctx = new ProductShopContext();
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            //Import
            Console.WriteLine(ImportUsers(ctx,
                File.ReadAllText($"../../../Datasets/users.xml")));
            Console.WriteLine(ImportProducts(ctx,
                File.ReadAllText($"../../../Datasets/products.xml")));
            Console.WriteLine(ImportCategories(ctx,
                File.ReadAllText($"../../../Datasets/categories.xml")));
            Console.WriteLine(ImportCategoryProducts(ctx,
                File.ReadAllText($"../../../Datasets/categories-products.xml")));
        }

        //s1.
        public static string ImportUsers(ProductShopContext context, string inputXml)
            => ImportBulkDataFromXml<User>(context, inputXml, ImportUsers, _ => true);

        //s2.
        public static string ImportProducts(ProductShopContext context, string inputXml)
            => ImportBulkDataFromXml<Product>(context, inputXml, ImportProducts, _ => true);

        //s3.
        public static string ImportCategories(ProductShopContext context, string inputXml)
            => ImportBulkDataFromXml<Category>(context, inputXml, ImportCategories, _ => true);

        //s4.
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
            => ImportBulkDataFromXml<CategoryProduct>(context, inputXml, ImportCategoryProducts,
                x => context.Categories.Any(c => c.Id == x.CategoryId) &&
                     context.Products.Any(p => p.Id == x.ProductId));

        public static string ImportBulkDataFromXml<T>(
            ProductShopContext context,
            string inputXml,
            Func<ProductShopContext, string, string> func,
            Predicate<T> predicate,
            string rootElement = null
        ) where T : class, new()
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
    }
}