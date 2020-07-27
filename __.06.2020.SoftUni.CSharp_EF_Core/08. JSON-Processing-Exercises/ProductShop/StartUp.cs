using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static JsonSerializerSettings JsonSerializingSettings { get; }
            = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

        public static void Main(string[] args)
        {
            var context = new ProductShopContext();
            // context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //Import
            // Console.WriteLine(ImportFromJson(context, ImportUsers));
            // Console.WriteLine(ImportFromJson(context, ImportProducts));
            // Console.WriteLine(ImportFromJson(context, ImportCategories));
            // Console.WriteLine(ImportFromJson(context, ImportCategoriesProducts));

            //Export
            // Console.WriteLine(GetProductsInRange(context));
            // Console.WriteLine(GetSoldProducts(context));
            // Console.WriteLine(GetCategoriesByProductsCount(context));
            Console.WriteLine(GetUsersWithProducts(context));
        }

        #region Imports

        //s1.
        public static string ImportUsers(ProductShopContext context, string inputJson)
            => DeserializeAddEntities<User>(context, inputJson, user =>
            {
                if (user?.LastName is null || user.LastName.Length < 3) return false;
                return true;
            });

        //s2.
        public static string ImportProducts(ProductShopContext context, string inputJson)
            => DeserializeAddEntities<Product>(context, inputJson, product =>
            {
                if (product?.Name is null || product.Name.Length < 3) return false;
                if (product.Price == 0m) return false;
                return true;
            });

        //s3.
        public static string ImportCategories(ProductShopContext context, string inputJson)
            => DeserializeAddEntities<Category>(context, inputJson, category =>
            {
                if (category?.Name is null || category.Name.Length < 3 || category.Name.Length > 15) return false;
                return true;
            });

        //s4.
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
            => DeserializeAddEntities<CategoryProduct>(context, inputJson, categoryProducts =>
            {
                if (categoryProducts is null) return false;
                return true;
            });

        public static string ImportCategoriesProducts(ProductShopContext context, string inputJson)
            => ImportCategoryProducts(context, inputJson);

        #region ImportDataHelpersRegion

        public static string DeserializeAddEntities<T>(ProductShopContext context, string inputJson,
            Predicate<T> predicate)
            where T : class, new()
        {
            var entities = JsonConvert
                .DeserializeObject<T[]>(inputJson)
                .Where(x => predicate(x))
                .ToArray();

            context.AddRange(entities);
            context.SaveChanges();
            return $"Successfully imported {entities.Count()}";
        }

        public static string ImportFromJson(
            ProductShopContext context,
            Func<ProductShopContext, string, string> func)
        {
            var inputJsonFile = string.Join("", func.Method.Name
                    .Replace("Import", "")
                    .Select(x => x.ToString())
                    .Select((x, i) => x.ToLower() != x && i != 0 ? $"-{x}" : x))
                .ToLower() + ".json";

            var inputJson = File.ReadAllText($"../../../Datasets/{inputJsonFile}");
            return func(context, inputJson);
        }

        #endregion

        #endregion

        //s5.
        public static string GetProductsInRange(ProductShopContext context)
            => JsonConvert.SerializeObject(
                context.Products
                    .OrderBy(p => p.Price)
                    .Where(p => p.Price >= 500 && p.Price <= 1000)
                    .Select(p => new
                    {
                        p.Name,
                        p.Price,
                        Seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                    }),
                JsonSerializingSettings
            );

        //s6.
        public static string GetSoldProducts(ProductShopContext context)
            => JsonConvert.SerializeObject(
                context.Users
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .Where(u => u.ProductsSold.Any(ps => ps.Buyer != null))
                    .Select(u => new
                    {
                        u.FirstName,
                        u.LastName,
                        SoldProducts = u.ProductsSold
                            .Select(ps => new
                            {
                                ps.Name,
                                ps.Price,
                                BuyerFirstName = ps.Buyer.FirstName,
                                BuyerLastName = ps.Buyer.LastName
                            })
                    })
                ,
                JsonSerializingSettings);

        //s7.
        public static string GetCategoriesByProductsCount(ProductShopContext context)
            => JsonConvert.SerializeObject(
                context.Categories
                    .OrderByDescending(c => c.CategoryProducts.Count)
                    .Select(c => new
                    {
                        Category = c.Name,
                        ProductsCount = c.CategoryProducts.Count,
                        AveragePrice = c.CategoryProducts
                            .Average(cp => cp.Product.Price)
                            .ToString("f2"),
                        TotalRevenue = c.CategoryProducts
                            .Sum(cp => cp.Product.Price)
                            .ToString("f2")
                    }),
                JsonSerializingSettings);

        //s8.
        #warning 8 Problem Not Finished
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Include(u => u.ProductsSold)
                .Where(u => u.ProductsSold.Any(ps => ps.BuyerId != null))
                .ToList()
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.Age,
                    SoldProducts = new
                    {
                        Count = u.ProductsSold
                            .Count(ps => ps.BuyerId != null),
                        Products = u.ProductsSold
                            .Where(ps => ps.BuyerId != null)
                            .Select(ps => new
                            {
                                ps.Name,
                                ps.Price
                            })
                    }
                })
                .OrderByDescending(u => u.SoldProducts.Count)
                .ToList();
            
            return JsonConvert.SerializeObject(
                new
                {
                    UsersCount = users.Count,
                    Users = users
                }
                ,
                new JsonSerializerSettings()
                {
                    Formatting = JsonSerializingSettings.Formatting,
                    ContractResolver = JsonSerializingSettings.ContractResolver,
                    NullValueHandling = NullValueHandling.Ignore
                }
            );
        }
    }
}