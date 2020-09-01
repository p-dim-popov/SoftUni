using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Export
{
    [XmlType("Game")]
    public class GameExportDto
    {
        [XmlAttribute("title")
        ]
        public string Title { get; set; }

        [XmlElement("Genre")
        ]
        public string Genre { get; set; }

        [XmlElement("Price")
        ]
        public decimal Price { get; set; }
    }

    [XmlType("Purchase")]
    public class PurchaseExportDto
    {
        public string Card { get; set; }
        public string Cvc { get; set; }
        public string Date { get; set; }
        public GameExportDto Game { get; set; }
    }

    [XmlType("User")]
    public class UserExportDto
    {
        [XmlAttribute("username")] public string Username { get; set; }
        public PurchaseExportDto[] Purchases { get; set; }
        public decimal TotalSpent { get; set; }
    }
}