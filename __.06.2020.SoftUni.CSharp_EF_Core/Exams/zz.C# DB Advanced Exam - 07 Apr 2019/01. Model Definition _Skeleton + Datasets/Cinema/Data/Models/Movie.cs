using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cinema.Data.Models.Enums;

namespace Cinema.Data.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [MaxLength(20),
         Required]
        public string Title { get; set; }

        public Genre Genre { get; set; }
        public TimeSpan Duration { get; set; }
        public double Rating { get; set; }

        [MaxLength(20),
         Required]
        public string Director { get; set; }

        public virtual ICollection<Projection> Projections { get; set; }
            = new HashSet<Projection>();
    }
}