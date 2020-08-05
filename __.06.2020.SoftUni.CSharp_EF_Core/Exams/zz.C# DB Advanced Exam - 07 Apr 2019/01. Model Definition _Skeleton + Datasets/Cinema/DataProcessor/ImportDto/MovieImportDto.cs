using System.ComponentModel.DataAnnotations;

namespace Cinema.DataProcessor.ImportDto
{
    public class MovieImportDto
    {
        [MinLength(3), MaxLength(20),
         Required]
        public string Title { get; set; }

        [Required
        ]
        public string Genre { get; set; }

        [Required
        ]
        public string Duration { get; set; }

        [Range(1.0, 10.0)
        ]
        public int Rating { get; set; }

        [MinLength(3), MaxLength(20),
         Required]
        public string Director { get; set; }
    }
}