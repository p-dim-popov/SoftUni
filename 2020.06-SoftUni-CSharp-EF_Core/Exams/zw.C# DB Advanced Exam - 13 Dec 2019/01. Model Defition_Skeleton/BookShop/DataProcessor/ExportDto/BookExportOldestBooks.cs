﻿using System.Xml.Serialization;

namespace BookShop.DataProcessor.ExportDto
{
    [XmlType("Book")]
    public class BookExportOldestBooks
    {
        [XmlAttribute("Pages")] public int Pages { get; set; }
        [XmlElement("Name")] public string Name { get; set; }
        [XmlElement("Date")] public string Date { get; set; }
    }
}