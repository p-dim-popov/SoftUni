using System.Xml.Serialization;

namespace ProductShop.Models
{
    using System.Collections.Generic;

    public class Product
    {
        public Product()
        {
            this.CategoryProducts = new List<CategoryProduct>();
        }

        [XmlElement("id")]
        public int Id { get; set; }
        
        [XmlElement("name")]
        public string Name { get; set; }
        
        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("sellerId")]
        public int SellerId { get; set; }
        
        [XmlIgnore]
        public User Seller { get; set; }

        [XmlElement("buyerId")]
        public int? BuyerId { get; set; }
        
        [XmlIgnore]
        public User Buyer { get; set; }

        [XmlIgnore]
        public ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}