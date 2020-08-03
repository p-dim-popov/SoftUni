using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace MusicHub.DataProcessor.ImportDtos
{
    [XmlType("Song")]
    public class SongImportDto
    {
            [XmlElement(ElementName="Name"),
            MinLength(3), MaxLength(20),
            Required]
            public string Name { get; set; }
            
            [XmlElement(ElementName="Duration"),
            Required]
            public string Duration { get; set; }
            
            [XmlElement(ElementName="CreatedOn"),
            Required]
            public string CreatedOn { get; set; }
            
            [XmlElement(ElementName="Genre"),
            Required]
            public string Genre { get; set; }
            
            [XmlElement(ElementName="AlbumId")]
            public string AlbumId { get; set; }
            
            [XmlElement(ElementName="WriterId"),
            Required]
            public int WriterId { get; set; }
            
            [XmlElement(ElementName="Price"),
            Range(typeof(decimal),
                "0",
                "79228162514264337593543950335"),
            Required]
            public decimal Price { get; set; }
    }
}