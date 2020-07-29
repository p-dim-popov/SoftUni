using System.Xml.Serialization;

namespace CarDealer.Models
{
    using System.Collections.Generic;

    public class Supplier
    {
        public Supplier()
        {
            this.Parts = new HashSet<Part>();
        }

        [XmlElement("id")] public int Id { get; set; }
        [XmlElement("name")] public string Name { get; set; }
        [XmlElement("isImporter")] public bool IsImporter { get; set; }
        [XmlIgnore] public ICollection<Part> Parts { get; set; }
    }
}