using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Cinema.Data.Models;
using Cinema.Data.Models.Enums;
using Cinema.DataProcessor.ImportDto;
using Newtonsoft.Json;

namespace Cinema.DataProcessor
{
    using System;
    using Data;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2}!";

        private const string SuccessfulImportHallSeat
            = "Successfully imported {0}({1}) with {2} seats!";

        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";

        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var jsonMovies = JsonConvert
                .DeserializeObject<MovieImportDto[]>(jsonString);

            var sb = new StringBuilder();

            var validMovies = new List<Movie>();
            foreach (var jsonMovie in jsonMovies)
            {
                if (!IsValid(jsonMovie))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isMovieDurationValid =
                    TimeSpan.TryParse(
                        jsonMovie.Duration,
                        out var movieDuration);
                if (!isMovieDurationValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (validMovies.Any(m => m.Title == jsonMovie.Title))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isMovieGenreValid =
                    Enum.TryParse(typeof(Genre), jsonMovie.Genre,
                        out var movieGenre);
                if (!isMovieGenreValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var movie = new Movie();
                movie.Title = jsonMovie.Title;
                movie.Genre = (Genre) movieGenre;
                movie.Duration = movieDuration;
                movie.Rating = jsonMovie.Rating;
                movie.Director = jsonMovie.Director;

                validMovies.Add(movie);
                sb.AppendLine(string.Format(
                    SuccessfulImportMovie,
                    movie.Title,
                    movie.Genre,
                    movie.Rating.ToString("f2")));
            }

            context.Movies.AddRange(validMovies);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var jsonHalls = JsonConvert
                .DeserializeObject<HallImportDto[]>(jsonString);

            var sb = new StringBuilder();
            var validHalls = new List<Hall>();
            foreach (var jsonHall in jsonHalls)
            {
                if (!IsValid(jsonHall))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var hall = new Hall();
                hall.Name = jsonHall.Name;
                hall.Is4Dx = jsonHall.Is4Dx;
                hall.Is3D = jsonHall.Is3D;
                hall.Seats = new Seat[jsonHall.Seats]
                    .Select(s => new Seat())
                    .ToList();

                var projectionType =
                    hall.Is3D && hall.Is4Dx
                        ? "4Dx/3D"
                        : hall.Is4Dx
                            ? "4Dx"
                            : hall.Is3D
                                ? "3D"
                                : "Normal";

                validHalls.Add(hall);
                sb.AppendLine(string.Format(
                    SuccessfulImportHallSeat,
                    hall.Name,
                    projectionType,
                    hall.Seats.Count));
            }

            context.Halls.AddRange(validHalls);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var xmlProjections =
                FromXmlTo<ProjectionImportDto[]>(xmlString, "Projections");

            var sb = new StringBuilder();
            var validProjections = new List<Projection>();
            foreach (var xmlProjection in xmlProjections)
            {
                if (!IsValid(xmlProjection))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isProjectionDateTimeValid =
                    DateTime.TryParseExact(xmlProjection.DateTime,
                        "yyyy-MM-dd HH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var projectionDateTime);
                if (!isProjectionDateTimeValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var movie = context.Movies
                    .FirstOrDefault(m => m.Id == xmlProjection.MovieId);
                if (movie is null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!context.Halls.Any(h => h.Id == xmlProjection.HallId))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var projection = new Projection();
                projection.MovieId = xmlProjection.MovieId;
                projection.HallId = xmlProjection.HallId;
                projection.DateTime = projectionDateTime;

                validProjections.Add(projection);

                sb.AppendLine(string.Format(
                    SuccessfulImportProjection,
                    movie.Title,
                    projectionDateTime
                        .ToString("MM/dd/yyyy",
                            CultureInfo.InvariantCulture)));
            }

            context.Projections.AddRange(validProjections);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var xmlCustomers =
                FromXmlTo<CustomerImportDto[]>(xmlString, "Customers");

            var sb = new StringBuilder();
            var validCustomers = new List<Customer>();
            foreach (var xmlCustomer in xmlCustomers)
            {
                if (!IsValid(xmlCustomer))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var customer = new Customer();
                customer.FirstName = xmlCustomer.FirstName;
                customer.LastName = xmlCustomer.LastName;
                customer.Age = xmlCustomer.Age;
                customer.Balance = xmlCustomer.Balance;

                bool hasInvalidTicket = false;
                foreach (var xmlTicket in xmlCustomer.Tickets.Collection)
                {
                    if (!IsValid(xmlTicket))
                    {
                        sb.AppendLine(ErrorMessage);
                        hasInvalidTicket = true;
                        break;
                    }

                    if (!context.Projections
                        .Any(p => p.Id == xmlTicket.ProjectionId))
                    {
                        sb.AppendLine(ErrorMessage);
                        hasInvalidTicket = true;
                        break;
                    }
                    
                    var ticket = new Ticket();
                    ticket.ProjectionId = xmlTicket.ProjectionId;
                    ticket.Price = xmlTicket.Price;
                    
                    customer.Tickets.Add(ticket);
                }

                if (hasInvalidTicket)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                validCustomers.Add(customer);
                sb.AppendLine(string.Format(
                    SuccessfulImportCustomerTicket,
                    customer.FirstName,
                    customer.LastName,
                    customer.Tickets.Count));
            }
            
            context.Customers.AddRange(validCustomers);
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