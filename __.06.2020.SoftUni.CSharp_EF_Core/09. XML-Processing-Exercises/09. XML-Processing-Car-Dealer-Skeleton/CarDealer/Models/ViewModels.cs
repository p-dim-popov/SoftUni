using System.Collections.Generic;
using System.Xml.Serialization;

namespace CarDealer.Models
{
    #region s11.

    [XmlRoot(ElementName = "partId")]
    public class PartIdImportCars
    {
        [XmlAttribute(AttributeName = "id")] public int Id { get; set; }
    }

    [XmlRoot(ElementName = "parts")]
    public class PartsImportCars
    {
        [XmlElement(ElementName = "partId")] public PartIdImportCars[] PartIds { get; set; }
    }

    [XmlRoot(ElementName = "Car")]
    public class CarImportCars
    {
        [XmlElement(ElementName = "make")] public string Make { get; set; }
        [XmlElement(ElementName = "model")] public string Model { get; set; }

        [XmlElement(ElementName = "TraveledDistance")]
        public long TraveledDistance { get; set; }

        [XmlElement(ElementName = "parts")] public PartsImportCars Parts { get; set; }
    }

    [XmlRoot(ElementName = "Cars")]
    public class CarsImportCars
    {
        [XmlElement(ElementName = "Car")] public CarImportCars[] Collection { get; set; }
    }

    #endregion

    //s14.
    [XmlType("car")]
    public class CarCarsWithDistance
    {
        [XmlElement("make")] public string Make { get; set; }
        [XmlElement("model")] public string Model { get; set; }
        [XmlElement("travelled-distance")] public long TravelledDistance { get; set; }
    }

    //s15.
    [XmlType("car")]
    public class CarCarsFromMakeBmw
    {
        [XmlAttribute("id")] public int Id { get; set; }
        [XmlAttribute("model")] public string Model { get; set; }
        [XmlAttribute("travelled-distance")] public long TravelledDistance { get; set; }
    }

    //s16.
    [XmlType("suplier")] //testing system has typo, so..
    public class SupplierLocalSuppliers
    {
        [XmlAttribute("id")] public int Id { get; set; }
        [XmlAttribute("name")] public string Name { get; set; }
        [XmlAttribute("parts-count")] public int PartsCount { get; set; }
    }

    #region s17.

    [XmlType("part")]
    public class PartCarsWithTheirParts
    {
        [XmlAttribute("name")] public string Name { get; set; }
        [XmlAttribute("price")] public decimal Price { get; set; }
    }

    [XmlType("car")]
    public class CarCarsWithTheirParts
    {
        [XmlAttribute("make")] public string Make { get; set; }
        [XmlAttribute("model")] public string Model { get; set; }
        [XmlAttribute("travelled-distance")] public long TravelledDistance { get; set; }

        [XmlArray("parts"), XmlArrayItem("part")]
        public PartCarsWithTheirParts[] Parts { get; set; }
    }

    #endregion

    //s18.
    [XmlType("customer")]
    public class CustomerTotalSalesByCustomer
    {
        [XmlAttribute("full-name")] public string FullName { get; set; }
        [XmlAttribute("bought-cars")] public int BoughtCars { get; set; }
        [XmlAttribute("spent-money")] public decimal SpentMoney { get; set; }
    }

    #region s19.

    [XmlType("car")]
    public class CarSalesWithAppliedDiscount
    {
        [XmlAttribute("make")] public string Make { get; set; }
        [XmlAttribute("model")] public string Model { get; set; }
        [XmlAttribute("travelled-distance")] public long TravelledDistance { get; set; }
    }

    [XmlType("sale")]
    public class SaleSalesWithAppliedDiscount
    {
        [XmlElement("car", typeof(CarSalesWithAppliedDiscount))]
        public CarSalesWithAppliedDiscount Car { get; set; }

        [XmlElement("discount")] public string Discount { get; set; }
        [XmlElement("customer-name")] public string CustomerName { get; set; }
        [XmlElement("price")] public string Price { get; set; }
        [XmlElement("price-with-discount")] public string PriceWithDiscount { get; set; }
    }

    #endregion
}