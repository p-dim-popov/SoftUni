using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaporStore.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(20),
         Required]
        public string Username { get; set; }

        [Column(TypeName = "varchar(max)"),
         Required]
        public string FullName { get; set; }

        [Required
        ]
        public string Email { get; set; }

        public int Age { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
            = new HashSet<Card>();
    }
}