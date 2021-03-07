using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace MusicHub.DataProcessor.ImportDtos
{
    [XmlType("Song")]
    public class SongPerformerImportDto {
        [XmlAttribute(AttributeName="id"),
        Required]
        public int Id { get; set; }
    }

    [XmlType("PerformersSongs")]
    public class PerformersSongsPerformerImportDto {
        [XmlElement(ElementName="Song")]
        public SongPerformerImportDto[] Collection { get; set; }
    }

    [XmlType("Performer")]
    public class PerformerPerformerImportDto {
        [XmlElement(ElementName="FirstName"),
        MinLength(3), MaxLength(20),
        Required]
        public string FirstName { get; set; }
        
        [XmlElement(ElementName="LastName"),
        MinLength(3), MaxLength(20),
        Required]
        public string LastName { get; set; }
        
        [XmlElement(ElementName="Age"),
        Range(18, 70),
        Required]
        public int Age { get; set; }
        
        [XmlElement(ElementName="NetWorth"),
        Range(typeof(decimal), 
            "0",
            "79228162514264337593543950335"),
        Required]
        public decimal NetWorth { get; set; }
        
        [XmlElement(ElementName="PerformersSongs")]
        public PerformersSongsPerformerImportDto PerformersSongs { get; set; }
    }
}