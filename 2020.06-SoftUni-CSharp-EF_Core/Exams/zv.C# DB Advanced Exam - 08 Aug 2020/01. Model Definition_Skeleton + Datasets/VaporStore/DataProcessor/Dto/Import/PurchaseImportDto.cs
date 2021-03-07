using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Import
{
    [XmlType("Purchase")]
    public class PurchaseImportDto
    {
        [XmlElement("Type"),
         Required,
         RegularExpression("^(Retail|Digital)$")]
        public string Type { get; set; }

        [XmlElement("Key"),
         Required,
         RegularExpression(@"^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$")]
        public string ProductKey { get; set; }

        [XmlElement("Card"),
         RegularExpression(@"^\d{4} \d{4} \d{4} \d{4}$"),
         Required
        ]
        public string Card { get; set; }

        [XmlElement("Date"),
         Required]
        public string Date { get; set; }

        [XmlAttribute("title"),
         Required]
        public string GameName { get; set; }
    }
}