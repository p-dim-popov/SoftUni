using System.ComponentModel.DataAnnotations;

namespace BookShop.DataProcessor.ImportDto
{
    public class BookAuthorImportDto
    {
        [Required
        ]
        public int? Id { get; set; }
    }

    public class AuthorImportDto
    {
        [MinLength(3), MaxLength(30),
         Required]
        public string FirstName { get; set; }

        [MinLength(3), MaxLength(30),
         Required]
        public string LastName { get; set; }

        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$"),
         Required]
        public string Phone { get; set; }

        [Required,
         EmailAddress]
        public string Email { get; set; }

        public BookAuthorImportDto[] Books { get; set; }
    }
}