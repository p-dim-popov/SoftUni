using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Ticket")]
    public class TicketImportDto
    {
        [XmlElement("ProjectionId")
        ]
        public int ProjectionId { get; set; }

        [XmlElement("Price"),
         Range(typeof(decimal), 
             "0.01",
             "79228162514264337593543950335")
        ]
        public decimal Price { get; set; }
    }

    [XmlType("Tickets")]
    public class TicketsCustomerImportDto
    {
        [XmlElement("Ticket")
        ]
        public TicketImportDto[] Collection { get; set; }
    }

    [XmlType("Customer")]
    public class CustomerImportDto
    {
        [XmlElement("FirstName"),
            MinLength(3), MaxLength(20),
            Required
        ]
        public string FirstName { get; set; }

        [XmlElement("LastName"),
            MinLength(3), MaxLength(20),
            Required
        ]
        public string LastName { get; set; }

        [XmlElement("Age"),
            Range(12, 110)
        ]
        public int Age { get; set; }

        [XmlElement("Balance"),
            Range(typeof(decimal),
                "0.01",
                "79228162514264337593543950335")
        ]
        public decimal Balance { get; set; }

        [XmlElement("Tickets")
        ]
        public TicketsCustomerImportDto Tickets { get; set; }
    }
}