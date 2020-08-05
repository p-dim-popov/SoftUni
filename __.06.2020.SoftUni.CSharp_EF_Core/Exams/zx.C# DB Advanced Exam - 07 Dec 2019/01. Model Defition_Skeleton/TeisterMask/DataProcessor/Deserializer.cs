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
            var projectDtos =
                FromXmlTo<ProjectImportProjectDto[]>(xmlString, "Projects");
            var sb = new StringBuilder();
            var validProjects = new List<Project>();
            foreach (var projectDto in projectDtos)
            {
                if (!IsValid(projectDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool hasInvalidOpenDate = !DateTime.TryParseExact(
                    projectDto.OpenDate,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var projectOpenDate);

                DateTime? projectDueDate = null;
                if (!string.IsNullOrWhiteSpace(projectDto.DueDate))
                {
                    if (!DateTime.TryParseExact(projectDto.DueDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var projectDueDateValue
                    ))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    projectDueDate = projectDueDateValue;
                }

                if (hasInvalidOpenDate ||
                    projectDueDate != null && projectDueDate < projectOpenDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var project = new Project();
                project.Name = projectDto.Name;
                project.OpenDate = projectOpenDate;
                project.DueDate = projectDueDate;

                foreach (var taskDto in projectDto.Tasks.Collection)
                {
                    if (!IsValid(taskDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool hasInvalidTaskOpenDate = !DateTime.TryParseExact(
                        taskDto.OpenDate,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var taskOpenDate
                    );

                    bool hasInvalidTaskDueDate = !DateTime.TryParseExact(
                        taskDto.DueDate,
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var taskDueDate
                    );

                    if (hasInvalidTaskOpenDate ||
                        hasInvalidTaskDueDate ||
                        taskOpenDate < project.OpenDate ||
                        project.DueDate.HasValue && taskDueDate > project.DueDate
                        /*taskOpenDate > taskDueDate*/)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var task = new Task();
                    task.Name = taskDto.Name;
                    task.OpenDate = taskOpenDate;
                    task.DueDate = taskDueDate;
                    task.ExecutionType = (ExecutionType) taskDto.ExecutionType;
                    task.LabelType = (LabelType) taskDto.LabelType;
                    // task.Project = project;

                    project.Tasks.Add(task);
                }

                validProjects.Add(project);
                
                sb.AppendLine(string.Format(
                    SuccessfullyImportedProject,
                    project.Name,
                    project.Tasks.Count
                ));
            }

            context.Projects.AddRange(validProjects);
            context.SaveChanges();
            
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
            var validEmployees = new List<Employee>();
            foreach (var jsonEmployee in jsonEmployees)
            {
                if (!IsValid(jsonEmployee))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var employee = new Employee();
                employee.Username = jsonEmployee.Username;
                employee.Email = jsonEmployee.Email;
                employee.Phone = jsonEmployee.Phone;

                foreach (var jsonTask in jsonEmployee.Tasks.Distinct())
                {
                    if (!context.Tasks.Any(t => t.Id == jsonTask))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var employeeTask = new EmployeeTask();
                    employeeTask.EmployeeId = employee.Id;
                    employeeTask.TaskId = jsonTask;

                    employee.EmployeesTasks.Add(employeeTask);
                }

                validEmployees.Add(employee);
                
                sb.AppendLine(string.Format(
                    SuccessfullyImportedEmployee,
                    employee.Username,
                    employee.EmployeesTasks.Count
                ));
            }

            context.Employees.AddRange(validEmployees);
            context.SaveChanges();
            
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