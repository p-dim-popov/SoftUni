using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProductShop.Models
{
    #region s5.

    [XmlType("Product")]
    public class ProductsInRange
    {
        [XmlElement("name")] public string Name { get; set; }
        [XmlElement("price")] public decimal Price { get; set; }
        [XmlElement("buyer")] public string Buyer { get; set; }
    }

    #endregion

    #region s6.

    [XmlType("Product")]
    public class SoldProduct
    {
        [XmlElement("name")] public string Name { get; set; }
        [XmlElement("price")] public decimal Price { get; set; }
    }

    [XmlType("User")]
    public class UserSoldProducts
    {
        [XmlElement("firstName")] public string FirstName { get; set; }
        [XmlElement("lastName")] public string LastName { get; set; }

        [XmlElement("soldProducts", typeof(SoldProduct[]))]
        public SoldProduct[] SoldProducts { get; set; }
    }

    #endregion

    #region s7.

    [XmlType("Category")]
    public class CategoryByProductsCount
    {
        [XmlElement("name")] public string Name { get; set; }
        [XmlElement("count")] public int Count { get; set; }
        [XmlElement("averagePrice")] public decimal AveragePrice { get; set; }
        [XmlElement("totalRevenue")] public decimal TotalRevenue { get; set; }
    }

    #endregion

    #region s8.

    [XmlType("Product")]
    public class SoldProductUsersAndProducts
    {
        [XmlElement("name")] public string Name { get; set; }
        [XmlElement("price")] public decimal Price { get; set; }
    }

    [XmlType("SoldProducts")]
    public class SoldProductsUsersAndProducts
    {
        [XmlElement("count")] public int Count { get; set; }

        [XmlElement("products", typeof(SoldProductUsersAndProducts[]))]
        public SoldProductUsersAndProducts[] Products { get; set; }
    }

    [XmlType("User")]
    public class UserUsersAndProducts
    {
        [XmlElement("firstName")] public string FirstName { get; set; }
        [XmlElement("lastName")] public string LastName { get; set; }
        [XmlElement("age")] public int? Age { get; set; }
        [XmlElement("SoldProducts")] public SoldProductsUsersAndProducts SoldProducts { get; set; }
    }

    [XmlType("Users")]
    public class UsersWithCountUsersAndProducts
    {
        [XmlElement("count")] public int Count { get; set; }

        [XmlElement("users", typeof(UserUsersAndProducts[]))]
        public UserUsersAndProducts[] Users { get; set; }
    }

    #endregion
}