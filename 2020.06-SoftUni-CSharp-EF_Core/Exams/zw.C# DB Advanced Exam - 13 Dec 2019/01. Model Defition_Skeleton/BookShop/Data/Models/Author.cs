﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Data.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30), Required] public string FirstName { get; set; }

        [MaxLength(30), Required] public string LastName { get; set; }

        [Required] public string Email { get; set; }
        [Required] public string Phone { get; set; }

        public virtual ICollection<AuthorBook> AuthorsBooks { get; set; }
            = new HashSet<AuthorBook>();
    }
}