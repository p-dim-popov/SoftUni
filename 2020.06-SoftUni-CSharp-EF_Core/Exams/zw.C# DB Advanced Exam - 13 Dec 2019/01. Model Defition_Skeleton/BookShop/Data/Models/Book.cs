using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookShop.Data.Models.Enums;

namespace BookShop.Data.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30), Required]
        public string Name { get; set; }

        public Genre Genre { get; set; }
        
        public decimal Price { get; set; }

        [MaxLength(5000)] public int Pages { get; set; }

        public DateTime PublishedOn { get; set; }

        public virtual ICollection<AuthorBook> AuthorsBooks { get; set; }
            = new HashSet<AuthorBook>();
    }
}