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
            Console.WriteLine(ImportUsers(ctx, null));
            Console.WriteLine(ImportProducts(ctx, null));
            Console.WriteLine(ImportCategories(ctx, null));
        }

        //s1.
        public static string ImportUsers(ProductShopContext context, string inputXml)
            => ImportBulkDataFromXml<User>(context, inputXml, "users.xml", "Users",
                user =>
                {
                    if (user?.Age < 0) return false;
                    return true;
                });

        //s2.
        public static string ImportProducts(ProductShopContext context, string inputXml)
            => ImportBulkDataFromXml<Product>(context, inputXml, "products.xml", "Products",
                product =>
                {
                    if (product?.Name is null) return false;
                    if (product.Price < 0) return false;
                    if (!context.Users.Any(u => u.Id.Equals(product.SellerId))) return false;
                    if (product.BuyerId != null)
                        if (!context.Users.Any(u => u.Id.Equals(product.BuyerId)))
                            return false;
                    return true;
                });

        //s3.
        public static string ImportCategories(ProductShopContext context, string inputXml)
            => ImportBulkDataFromXml<Category>(context, inputXml, "categories.xml", "Categories",
                _ => true);

        private static string ImportBulkDataFromXml<T>(
            ProductShopContext context,
            string inputXml,
            string filename,
            string rootAttribute,
            Predicate<T> predicate
        )
            where T : class, new()
        {
            var serializer = new XmlSerializer(typeof(T[]), new XmlRootAttribute(rootAttribute));
            var entities = (serializer
                        .Deserialize(
                            inputXml is null
                                ? (TextReader) new StreamReader($"../../../Datasets/{filename}")
                                : new StringReader(inputXml))
                    as T[])
                .Where(x => predicate(x)).
                ToList();

            foreach (var entity in entities)
            {
                context.Add<T>(entity);
            }
            
            context.SaveChanges();
            return $"Successfully imported {entities.Count}";
        }
    }
}