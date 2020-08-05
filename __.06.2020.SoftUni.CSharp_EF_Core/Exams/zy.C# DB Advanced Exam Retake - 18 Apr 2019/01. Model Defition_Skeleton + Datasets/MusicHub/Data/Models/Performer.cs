﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicHub.Data.Models
{
    public class Performer
    {
        public int Id { get; set; }
        [MaxLength(20), Required]
        public string FirstName { get; set; }
        [MaxLength(20), Required]
        public string LastName { get; set; }
        public int Age { get; set; }
        public decimal NetWorth { get; set; }
        public virtual ICollection<SongPerformer> PerformerSongs { get; set; }
        = new HashSet<SongPerformer>();
    }
}