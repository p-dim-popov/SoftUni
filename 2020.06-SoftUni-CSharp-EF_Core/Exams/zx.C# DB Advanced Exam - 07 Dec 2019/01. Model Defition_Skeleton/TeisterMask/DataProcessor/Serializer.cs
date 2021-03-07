using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TeisterMask.Data.Models.Enums;
using TeisterMask.DataProcessor.ExportDto;

namespace TeisterMask.DataProcessor
{
    using System;
    using Data;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            //Won't work in judge system without including and materializing the whole data
            var projects = context.Projects
                .Include(p => p.Tasks)
                .ToArray()
                .Where(p => p.Tasks.Any())
                .Select(p => new ProjectExportDto
                {
                    ProjectName = p.Name, 
                    TasksCount = p.Tasks.Count,
                    HasEndDate = p.DueDate.HasValue ? "Yes" : "No",
                    Tasks = p.Tasks
                        .Select(t => new TaskExportDto
                        {
                            Name = t.Name,
                            Label = t.LabelType.ToString()
                        })
                        .OrderBy(t => t.Name)
                        .ToArray()
                })
                .OrderByDescending(p => p.Tasks.Length)
                .ThenBy(p => p.ProjectName)
                .ToArray();

            return ToXml(projects, "Projects");
        }

        private static string ToXml<T>(ICollection<T> data, string rootElement = null)
            where T : new()
        {
            var xmls = string.IsNullOrWhiteSpace(rootElement)
                    ? new XmlSerializer(data.GetType())
                : new XmlSerializer(data.GetType(), new XmlRootAttribute(rootElement));
            using (var sw = new StringWriter())
            using (var xmlw = XmlWriter.Create(sw,
                new XmlWriterSettings {Indent = true}))
            {
                var xmlns = new XmlSerializerNamespaces();
                xmlns.Add("", "");
                xmls.Serialize(xmlw, data, xmlns);
                return  sw.ToString();
            }
        }

        
        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            //Won't work in judge system without including and materializing the whole data
            var employees = context.Employees
                .Include(e => e.EmployeesTasks)
                .Include("EmployeesTasks.Task")
                .ToArray()
                .Where(e => e.EmployeesTasks
                    .Any(et => et.Task.OpenDate >= date))
                .Select(e => new
                {
                    e.Username,
                    Tasks = e.EmployeesTasks
                        .Select(et => et.Task)
                        .Where(t => t.OpenDate >= date)
                        .OrderByDescending(t => t.DueDate)
                        .ThenBy(t => t.Name)
                        .Select(t => new
                        {
                            TaskName = t.Name,
                            OpenDate = t.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                            DueDate = t.DueDate.ToString("d", CultureInfo.InvariantCulture),
                            LabelType = t.LabelType.ToString(),
                            ExecutionType = t.ExecutionType.ToString()
                        })
                        .ToArray()
                })
                .OrderByDescending(e => e.Tasks.Length)
                .ThenBy(e => e.Username)
                .Take(10)
                .ToArray();

            var json = JsonConvert.SerializeObject(employees, Formatting.Indented);
            return json;
        }
    }
}