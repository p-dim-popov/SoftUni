using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Xml.Serialization;
using Castle.Components.DictionaryAdapter;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Task")]
    public class TaskImportProjectDto
    {
        [XmlElement("Name"),
         MinLength(2), MaxLength(40),
         Required]
        public string Name { get; set; }

        [XmlElement("OpenDate"),
         Required]
        public string OpenDate { get; set; }

        [XmlElement("DueDate"),
         Required]
        public string DueDate { get; set; }

        [XmlElement("ExecutionType"),
         Range(0, 3)]
        public int ExecutionType { get; set; }

        [XmlElement("LabelType"),
         Range(0, 4)]
        public int LabelType { get; set; }
    }

    [XmlType("Tasks")]
    public class TasksImportProjectDto
    {
        [XmlElement("Task")] public TaskImportProjectDto[] Collection { get; set; }
    }

    [XmlType("Project")]
    public class ProjectImportProjectDto
    {
        [XmlElement("Name"),
         MinLength(2), MaxLength(40),
         Required]
        public string Name { get; set; }

        [XmlElement("OpenDate"),
         Required]
        public string OpenDate { get; set; }

        [XmlElement("DueDate")
        ]
        public string DueDate { get; set; }

        [XmlElement("Tasks")
        ]
        public TasksImportProjectDto Tasks { get; set; }
    }
}