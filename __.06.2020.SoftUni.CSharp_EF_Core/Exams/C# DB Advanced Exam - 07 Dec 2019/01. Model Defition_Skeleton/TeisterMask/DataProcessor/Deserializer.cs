using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using TeisterMask.Data.Models;
using TeisterMask.Data.Models.Enums;
using TeisterMask.DataProcessor.ImportDto;

namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
    using Data;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var projectDtos = FromXmlTo<ProjectImportProjectDto[]>(xmlString, "Projects");
            var sb = new StringBuilder();
            var validProjects = new List<Project>();
            foreach (var projectDto in projectDtos)
            {
                if (!IsValid(projectDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (!DateTime.TryParseExact(
                    projectDto.OpenDate,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var projectOpenDate
                ))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }


                DateTime? projectDueDate = null;
                if (!string.IsNullOrWhiteSpace(projectDto.DueDate))
                {
                    if (!DateTime.TryParseExact(projectDto.DueDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var dueDate
                    ))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    projectDueDate = dueDate;
                }

                if (projectDueDate != null && projectDueDate < projectOpenDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                
                var project = new Project
                {
                    Name = projectDto.Name,
                    OpenDate = projectOpenDate,
                    DueDate = projectDueDate
                };

                context.Projects.Add(project);
                context.SaveChanges();

                var taskDtos = projectDto.Tasks.Collection;
                var validTasks = new List<Task>();
                foreach (var taskDto in taskDtos)
                {
                    if (!IsValid(taskDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (!Enum.IsDefined(typeof(ExecutionType), taskDto.ExecutionType))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (!Enum.IsDefined(typeof(LabelType), taskDto.LabelType))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (!DateTime.TryParseExact(
                        taskDto.OpenDate,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var taskOpenDate
                    ))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (!DateTime.TryParseExact(
                        taskDto.DueDate,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var taskDueDate
                    ))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (taskOpenDate < project.OpenDate ||
                        project.DueDate.HasValue && taskDueDate > project.DueDate || 
                        taskOpenDate > taskDueDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var task = new Task
                    {
                        Name = taskDto.Name,
                        OpenDate = taskOpenDate,
                        DueDate = taskDueDate,
                        ExecutionType = (ExecutionType) taskDto.ExecutionType,
                        LabelType = (LabelType) taskDto.LabelType,
                        ProjectId = project.Id
                    };

                    validTasks.Add(task);
                }

                context.Tasks.AddRange(validTasks);
                context.SaveChanges();

                sb.AppendLine(string.Format(
                    SuccessfullyImportedProject,
                    project.Name,
                    validTasks.Count
                ));
            }

            return sb.ToString();
        }

        private static T FromXmlTo<T>(string xmlString, string rootElement = null)
            where T : class
        {
            XmlSerializer xmls = string.IsNullOrWhiteSpace(rootElement)
                ? xmls = new XmlSerializer(typeof(T))
                : xmls = new XmlSerializer(typeof(T), new XmlRootAttribute(rootElement));

            using (var strr = new StringReader(xmlString))
                return xmls.Deserialize(strr) as T;
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var jsonEmployees = JsonConvert
                .DeserializeObject<EmployeeImportEmployeesDto[]>(jsonString);

            var sb = new StringBuilder();
            foreach (var jsonEmployee in jsonEmployees)
            {
                if (!IsValid(jsonEmployee))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var employee = new Employee
                {
                    Username = jsonEmployee.Username,
                    Email = jsonEmployee.Email,
                    Phone = jsonEmployee.Phone
                };

                context.Employees.Add(employee);
                context.SaveChanges();

                var jsonTasks = jsonEmployee.Tasks
                    .Distinct()
                    .ToArray();
                var validEmployeesTasks = new List<EmployeeTask>();
                foreach (var jsonTask in jsonTasks)
                {
                    if (!context.Tasks.Any(t => t.Id == jsonTask))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var employeeTask = new EmployeeTask
                    {
                        EmployeeId = employee.Id,
                        TaskId = jsonTask
                    };

                    validEmployeesTasks.Add(employeeTask);
                }

                context.EmployeesTasks.AddRange(validEmployeesTasks);
                context.SaveChanges();
                sb.AppendLine(string.Format(
                    SuccessfullyImportedEmployee,
                    employee.Username,
                    validEmployeesTasks.Count
                ));
            }

            return sb.ToString();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}