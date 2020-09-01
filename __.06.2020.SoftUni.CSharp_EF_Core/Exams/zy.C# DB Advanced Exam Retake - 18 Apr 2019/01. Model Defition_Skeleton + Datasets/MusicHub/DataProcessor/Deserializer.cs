using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.DataProcessor.ImportDtos;
using Newtonsoft.Json;

namespace MusicHub.DataProcessor
{
    using System;
    using Data;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data";

        private const string SuccessfullyImportedWriter
            = "Imported {0}";

        private const string SuccessfullyImportedProducerWithPhone
            = "Imported {0} with phone: {1} produces {2} albums";

        private const string SuccessfullyImportedProducerWithNoPhone
            = "Imported {0} with no phone number produces {1} albums";

        private const string SuccessfullyImportedSong
            = "Imported {0} ({1} genre) with duration {2}";

        private const string SuccessfullyImportedPerformer
            = "Imported {0} ({1} songs)";

        public static string ImportWriters(MusicHubDbContext context, string jsonString)
        {
            var jsonWriters = JsonConvert
                .DeserializeObject<WriterImportDto[]>(jsonString);

            var sb = new StringBuilder();
            foreach (var jsonWriter in jsonWriters)
            {
                if (!IsValid(jsonWriter))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var writer = new Writer();
                writer.Name = jsonWriter.Name;
                writer.Pseudonym = jsonWriter.Pseudonym;
                context.Writers.Add(writer);
                sb.AppendLine(string.Format(SuccessfullyImportedWriter, writer.Name));
            }

            context.SaveChanges();
            return sb.ToString();
        }


        public static string ImportProducersAlbums(MusicHubDbContext context, string jsonString)
        {
            var jsonProducers = JsonConvert
                .DeserializeObject<ProducerImportDto[]>(jsonString);

            var sb = new StringBuilder();
            var validProducers = new List<Producer>();
            foreach (var jsonProducer in jsonProducers)
            {
                if (!IsValid(jsonProducer))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var validAlbums = new List<Album>();
                var hasInvalidAlbum = false;
                foreach (var jsonAlbum in jsonProducer.Albums)
                {
                    if (!IsValid(jsonAlbum))
                    {
                        hasInvalidAlbum = true;
                        sb.AppendLine(ErrorMessage);
                        break;
                    }

                    bool hasInvalidReleaseDate =
                        !DateTime.TryParseExact(jsonAlbum.ReleaseDate,
                            "dd/MM/yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out var albumReleaseDate);

                    if (hasInvalidReleaseDate)
                    {
                        hasInvalidAlbum = true;
                        sb.AppendLine(ErrorMessage);
                        break;
                    }
                    
                    var album = new Album();
                    album.Name = jsonAlbum.Name;
                    album.ReleaseDate = albumReleaseDate;
                    validAlbums.Add(album);
                }

                if (hasInvalidAlbum)
                    continue;

                var producer = new Producer();
                producer.Name = jsonProducer.Name;
                producer.Pseudonym = jsonProducer.Pseudonym;
                producer.PhoneNumber = jsonProducer.PhoneNumber;
                producer.Albums = validAlbums;
                
                validProducers.Add(producer);

                if (!string.IsNullOrWhiteSpace(producer.PhoneNumber))
                    sb.AppendLine(string.Format(
                        SuccessfullyImportedProducerWithPhone,
                        producer.Name,
                        producer.PhoneNumber,
                        validAlbums.Count));
                else
                    sb.AppendLine(string.Format(
                        SuccessfullyImportedProducerWithNoPhone,
                        producer.Name,
                        validAlbums.Count));
            }

            context.Producers.AddRange(validProducers);
            context.SaveChanges();
            
            return sb.ToString();
        }

        public static string ImportSongs(MusicHubDbContext context, string xmlString)
        {
            var xmlSongs =
                FromXmlTo<SongImportDto[]>(xmlString, "Songs");

            var sb = new StringBuilder();
            var validSongs = new List<Song>();
            foreach (var xmlSong in xmlSongs)
            {
                if (!IsValid(xmlSong))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool hasInvalidDuration =
                    !TimeSpan.TryParseExact(
                        xmlSong.Duration,
                        "c",
                        CultureInfo.InvariantCulture,
                        TimeSpanStyles.None,
                        out var songDuration
                    );

                bool hasInvalidCreatedOn =
                    !DateTime.TryParseExact(
                        xmlSong.CreatedOn,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var songCreatedOn);

                bool hasInvalidGenre =
                    !Enum.TryParse(typeof(Genre),
                        xmlSong.Genre,
                        out var songGenre);

                int? songAlbumId = null;
                if (!string.IsNullOrWhiteSpace(xmlSong.AlbumId))
                {
                    bool hasInvalidAlbumId =
                        !int.TryParse(xmlSong.AlbumId,
                            out var songAlbumIdValue);

                    if (hasInvalidAlbumId)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    
                    songAlbumId = songAlbumIdValue;
                    if (!context.Albums
                        .Any(a => a.Id == songAlbumId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                }

                bool hasInvalidWriter = !context.Writers
                    .Any(w => w.Id == xmlSong.WriterId);

                if (hasInvalidDuration ||
                    hasInvalidCreatedOn ||
                    hasInvalidGenre ||
                    hasInvalidWriter)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var song = new Song();
                song.Name = xmlSong.Name;
                song.Duration = songDuration;
                song.CreatedOn = songCreatedOn;
                song.Genre = (Genre) songGenre;
                song.AlbumId = songAlbumId;
                song.WriterId = xmlSong.WriterId;
                song.Price = xmlSong.Price;

                validSongs.Add(song);
                sb.AppendLine(string.Format(
                    SuccessfullyImportedSong,
                    song.Name,
                    song.Genre.ToString(),
                    song.Duration.ToString("c")));
            }

            context.Songs.AddRange(validSongs);
            context.SaveChanges();
            return sb.ToString();
        }

        public static string ImportSongPerformers(MusicHubDbContext context, string xmlString)
        {
            var xmlPerformers =
                FromXmlTo<PerformerPerformerImportDto[]>(
                    xmlString,
                    "Performers"
                );

            var sb = new StringBuilder();
            var validPerformers = new List<Performer>();
            foreach (var xmlPerformer in xmlPerformers)
            {
                if (!IsValid(xmlPerformer))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool hasInvalidSong = false;
                foreach (var xmlSong in xmlPerformer.PerformersSongs.Collection)
                {
                    if (!IsValid(xmlSong))
                    {
                        sb.AppendLine(ErrorMessage);
                        hasInvalidSong = true;
                        break;
                    }

                    if (!context.Songs.Any(s => s.Id == xmlSong.Id))
                    {
                        sb.AppendLine(ErrorMessage);
                        hasInvalidSong = true;
                        break;
                    }
                }

                if (hasInvalidSong)
                    continue;

                var performer = new Performer();
                performer.FirstName = xmlPerformer.FirstName;
                performer.LastName = xmlPerformer.LastName;
                performer.Age = xmlPerformer.Age;
                performer.NetWorth = xmlPerformer.NetWorth;

                performer.PerformerSongs = xmlPerformer.PerformersSongs.Collection
                    .Select(s => new SongPerformer
                    {
                        SongId = s.Id
                    })
                    .ToArray();

                sb.AppendLine(
                    string.Format(
                        SuccessfullyImportedPerformer,
                        performer.FirstName,
                        performer.PerformerSongs.Count));
                validPerformers.Add(performer);
            }

            context.Performers.AddRange(validPerformers);
            context.SaveChanges();
            
            return sb.ToString();
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

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}