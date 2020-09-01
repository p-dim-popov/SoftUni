using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class CardImportDto
    {
        [RegularExpression(@"^\d{4} \d{4} \d{4} \d{4}$"),
         Required
        ]
        public string Number { get; set; }

        [RegularExpression(@"\d{3}"),
         Required
        ]
        public string CVC { get; set; }

        [Required,
         RegularExpression(@"^(Debit|Credit)$")
        ]
        public string Type { get; set; }
    }

    public class UserImportDto
    {
        [RegularExpression(@"^[A-Z][a-z]*? [A-Z][a-z]*$"),
         Required
        ]
        public string FullName { get; set; }

        [MinLength(3), MaxLength(20),
         Required
        ]
        public string Username { get; set; }

        [Required,
         EmailAddress
        ]
#warning may not require validation!!!
        public string Email { get; set; }

        [Range(3, 103)] public int Age { get; set; }
        public CardImportDto[] Cards { get; set; }
    }
}