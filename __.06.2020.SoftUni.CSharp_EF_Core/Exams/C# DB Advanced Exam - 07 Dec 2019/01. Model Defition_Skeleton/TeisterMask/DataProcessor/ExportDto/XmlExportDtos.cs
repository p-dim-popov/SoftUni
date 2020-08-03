using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Task")]
    public class TaskExportDto
    {
        public string Name { get; set; }
        public string Label { get; set; }
    }

    [XmlType("Project")]
    public class ProjectExportDto
    {
        public string ProjectName { get; set; }
        [XmlAttribute]
        public int TasksCount { get; set; }
        public string HasEndDate { get; set; }
        public TaskExportDto[] Tasks { get; set; }
    }
}