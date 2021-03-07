using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [MaxLength(20),
         Required]
        public string FirstName { get; set; }

        [MaxLength(20),
         Required]
        public string LastName { get; set; }

        public int Age { get; set; }
        public decimal Balance { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
            = new HashSet<Ticket>();
    }
}