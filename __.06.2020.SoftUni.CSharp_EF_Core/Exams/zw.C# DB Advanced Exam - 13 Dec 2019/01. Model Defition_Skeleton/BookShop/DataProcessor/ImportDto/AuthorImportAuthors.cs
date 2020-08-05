using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookShop.Data.Models;
using Newtonsoft.Json;

namespace BookShop.DataProcessor.ImportDto
{
        public class BookImportAuthors    {
            [JsonProperty("Id")]
            public int? Id { get; set; } 
        }

        public class AuthorImportAuthors    {
            [JsonProperty("FirstName"), Required, MinLength(3), MaxLength(30)]
            public string FirstName { get; set; } 

            [JsonProperty("LastName"), Required, MinLength(3), MaxLength(30)]
            public string LastName { get; set; } 

            [JsonProperty("Phone"), RegularExpression(@"^\d{3}-\d{3}-\d{4}$")]
            public string Phone { get; set; } 

            [JsonProperty("Email"), Required, EmailAddress]
            public string Email { get; set; } 

            [JsonProperty("Books"), Required]
            public BookImportAuthors[] Books { get; set; } 
        }
}