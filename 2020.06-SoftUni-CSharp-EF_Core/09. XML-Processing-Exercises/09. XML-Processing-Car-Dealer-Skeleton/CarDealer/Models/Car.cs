using System.Xml.Serialization;

namespace CarDealer.Models
{
    using System.Collections.Generic;

    public class Car
    {
        public Car()
        {
            this.PartCars = new HashSet<PartCar>();
        }

        [XmlElement("id")] public int Id { get; set; }
        [XmlElement("make")] public string Make { get; set; }
        [XmlElement("model")] public string Model { get; set; }
        [XmlElement("travelledDistance")] public long TravelledDistance { get; set; }
        [XmlIgnore] public ICollection<Sale> Sales { get; set; }
        [XmlIgnore] public ICollection<PartCar> PartCars { get; set; }
    }
}