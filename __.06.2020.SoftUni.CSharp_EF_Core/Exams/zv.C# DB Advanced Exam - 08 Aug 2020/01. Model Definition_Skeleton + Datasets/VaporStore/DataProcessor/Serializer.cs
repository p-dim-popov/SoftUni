using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VaporStore.Data.Models;
using VaporStore.DataProcessor.Dto.Export;
using Formatting = Newtonsoft.Json.Formatting;

namespace VaporStore.DataProcessor
{
    using System;
    using Data;

    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var games = context.Genres
                .Include(g => g.Games)
                .Include("Games.Purchases")
                .Include("Games.Developer")
                .Include("Games.GameTags.Tag")
                .ToArray()
                .Where(g => genreNames.Contains(g.Name) &&
                            g.Games
                                .Any(game => game.Purchases
                                    .Any()))
                .Select(genre => new
                {
                    genre.Id,
                    Genre = genre.Name,
                    Games = genre.Games
                        .Select(game => new
                        {
                            game.Id,
                            Title = game.Name,
                            Developer = game.Developer.Name,
                            Tags = string.Join(", ",
                                game
                                    .GameTags
                                    .Select(gt => gt.Tag.Name)),
                            Players = game.Purchases.Count
                        })
                        .Where(game => game.Players > 0)
                        .OrderByDescending(game => game.Players)
                        .ThenBy(game => game.Id)
                        .ToArray(),
                    TotalPlayers = genre.Games
                        .Sum(game => game
                            .Purchases
                            .Count)
                })
                .OrderByDescending(genre => genre.TotalPlayers)
                .ThenBy(genre => genre.Id)
                .ToArray();

            return JsonConvert.SerializeObject(games, Formatting.Indented);
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            var users = context.Users
                .Include(u => u.Cards)
                .Include("Cards.Purchases")
                .Include("Cards.Purchases.Card")
                .Include("Cards.Purchases.Game")
                .Include("Cards.Purchases.Game.Genre")
                .ToArray()
                .Select(u => new
                {
                    u.Username,
                    u.Cards,
                    Purchases = u.Cards
                        .Select(c => c.Purchases)
                        .Aggregate(new List<Purchase>(),
                            (list, purchases) => list.Concat(purchases).ToList())
                })
                .Where(u => u.Purchases
                    .Any(p => p.Type.ToString() == storeType))
                .Select(u => new UserExportDto
                {
                    Username = u.Username,
                    Purchases = u.Purchases
                        .Where(p => p.Type.ToString() == storeType)
                        .Select(p => new PurchaseExportDto
                        {
                            Card = p.Card.Number,
                            Cvc = p.Card.Cvc,
                            Date = p.Date.ToString("yyyy-MM-dd HH:mm",
                                CultureInfo.InvariantCulture),
                            Game = new GameExportDto
                            {
                                Title = p.Game.Name,
                                Genre = p.Game.Genre.Name,
                                Price = p.Game.Price
                            }
                        })
                        .OrderBy(p => p.Date)
                        .ToArray(),
                    TotalSpent = u.Cards
                        .Select(c => c.Purchases)
                        .Aggregate(new List<Purchase>(),
                            (list, purchases) => list.Concat(purchases).ToList())
                        .Where(p => p.Type.ToString() == storeType)
                        .Sum(p => p.Game.Price)
                })
                .OrderByDescending(u => u.TotalSpent)
                .ThenBy(u => u.Username)
                .ToArray();

            return ToXml(users, "Users");
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