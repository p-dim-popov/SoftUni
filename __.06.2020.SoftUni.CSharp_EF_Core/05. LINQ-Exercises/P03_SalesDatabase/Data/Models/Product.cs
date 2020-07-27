using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace P03_SalesDatabase.Data.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [MaxLength(50)] public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        [DefaultValue("No description")] public string Description { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
            = new HashSet<Sale>();
    }
}