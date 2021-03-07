using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using VaporStore.Data.Models;
using VaporStore.Data.Models.Enums;
using VaporStore.DataProcessor.Dto.Import;

namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data;

    public static class Deserializer
    {
        public const string ErrorMessage = "Invalid Data";

        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var jsonGames = JsonConvert
                .DeserializeObject<GameImportDto[]>(jsonString);

            var sb = new StringBuilder();
            var validGames = new List<Game>();
            foreach (var jsonGame in jsonGames)
            {
                if (!IsValid(jsonGame))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!jsonGame.Tags.Any())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (jsonGame.Tags.Any(string.IsNullOrWhiteSpace))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isGameReleaseDateValid =
                    DateTime.TryParseExact(jsonGame.ReleaseDate,
                        "yyyy-MM-dd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var gameReleaseDateValue);
                if (!isGameReleaseDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var developer = validGames
                    .Select(g => g.Developer)
                    .FirstOrDefault(d => d.Name == jsonGame.Developer);
                if (developer is null)
                {
                    developer = new Developer();
                    developer.Name = jsonGame.Developer;
                }

                var genre = validGames
                    .Select(g => g.Genre)
                    .FirstOrDefault(g => g.Name == jsonGame.Genre);
                if (genre is null)
                {
                    genre = new Genre();
                    genre.Name = jsonGame.Genre;
                }

                var game = new Game();
                game.Name = jsonGame.Name;
                game.Price = jsonGame.Price;
                game.ReleaseDate = gameReleaseDateValue;
                game.Developer = developer;
                game.Genre = genre;

                foreach (var jsonTag in jsonGame.Tags)
                {
                    var arrayOfArrayOfTags = validGames
                        .Select(g => g.GameTags
                            .Select(gt => gt.Tag))
                        .FirstOrDefault(arr => arr
                            .FirstOrDefault(t => t.Name == jsonTag) != null);

                    var tag = arrayOfArrayOfTags
                        ?.FirstOrDefault(t => t.Name == jsonTag);
                    if (tag is null)
                    {
                        tag = new Tag();
                        tag.Name = jsonTag;
                    }

                    var gameTag = new GameTag();
                    gameTag.Tag = tag;

                    game.GameTags.Add(gameTag);
                }

                validGames.Add(game);
                sb.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {game.GameTags.Count} tags");
            }

            context.Games.AddRange(validGames);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            var jsonUsers = JsonConvert
                .DeserializeObject<UserImportDto[]>(jsonString);

            var sb = new StringBuilder();
            var validUsers = new List<User>();

            foreach (var jsonUser in jsonUsers)
            {
                if (!IsValid(jsonUser))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!jsonUser.Cards.Any())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var user = new User();
                user.FullName = jsonUser.FullName;
                user.Username = jsonUser.Username;
                user.Email = jsonUser.Email;
                user.Age = jsonUser.Age;

                bool hasInvalidCard = false;
                foreach (var jsonCard in jsonUser.Cards)
                {
                    if (!IsValid(jsonCard))
                    {
                        sb.AppendLine(ErrorMessage);
                        hasInvalidCard = true;
                        break;
                    }

                    bool isCardTypeValid = Enum
                        .TryParse<CardType>(jsonCard.Type,
                            out var cardTypeValue);
                    if (!isCardTypeValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        hasInvalidCard = true;
                        break;
                    }

                    var card = new Card();
                    card.Number = jsonCard.Number;
                    card.Cvc = jsonCard.CVC;
                    card.Type = cardTypeValue;

                    user.Cards.Add(card);
                }

                if (hasInvalidCard)
                    continue;

                validUsers.Add(user);
                sb.AppendLine($"Imported {user.Username} with {user.Cards.Count} cards");
            }
            
            context.Users.AddRange(validUsers);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            var xmlPurchases =
                FromXmlTo<PurchaseImportDto[]>(xmlString, "Purchases");

            var sb = new StringBuilder();
            var validPurchases = new List<Purchase>();

            foreach (var xmlPurchase in xmlPurchases)
            {
                if (!IsValid(xmlPurchase))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var game = context.Games
                    .FirstOrDefault(g => g.Name == xmlPurchase.GameName);
                if (game is null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isPurchaseTypeValid =
                    Enum.TryParse<PurchaseType>(xmlPurchase.Type,
                        out var purchaseTypeValue);
                if (!isPurchaseTypeValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var card = context.Cards
                    .FirstOrDefault(c => c.Number == xmlPurchase.Card);
                if (card is null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isPurchaseDateValid =
                    DateTime.TryParseExact(xmlPurchase.Date,
                        "dd/MM/yyyy HH:mm",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var purchaseDateValue);
                if (!isPurchaseDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                
                var purchase = new Purchase();
                purchase.Type = purchaseTypeValue;
                purchase.ProductKey = xmlPurchase.ProductKey;
                purchase.Date = purchaseDateValue;
                purchase.Card = card;
                purchase.Game = game;

                var user = context.Users
                    .First(u => u.Cards
                        .FirstOrDefault(c => c.Number == purchase.Card.Number) != null);

                validPurchases.Add(purchase);
                sb.AppendLine($"Imported {purchase.Game.Name} for {user.Username}");
            }
            
            context.Purchases.AddRange(validPurchases);
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
                return  sw.ToString();
            }
        }

    }
}