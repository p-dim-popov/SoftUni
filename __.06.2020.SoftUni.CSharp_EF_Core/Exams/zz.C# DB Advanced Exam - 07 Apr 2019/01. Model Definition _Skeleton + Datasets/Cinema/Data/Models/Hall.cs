using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Data.Models
{
    public class Hall
    {
        public int Id { get; set; }

        [MaxLength(20),
         Required]
        public string Name { get; set; }

        public bool Is4Dx { get; set; }
        public bool Is3D { get; set; }

        public virtual ICollection<Projection> Projections { get; set; }
            = new HashSet<Projection>();

        public virtual ICollection<Seat> Seats { get; set; }
            = new HashSet<Seat>();
    }
}