using SoftUni.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using SoftUni.Models;

namespace SoftUni
{
    //2.Database First
    public static class StartUp
    {
        public static void Main()
        {
            //Console.WriteLine(GetEmployeesFullInformation(new SoftUniContext()));
            //Console.WriteLine(GetEmployeesWithSalaryOver50000(new SoftUniContext()));
            //Console.WriteLine(GetEmployeesFromResearchAndDevelopment(new SoftUniContext()));
            //Console.WriteLine(AddNewAddressToEmployee(new SoftUniContext()));
            //Console.WriteLine(GetEmployeesInPeriod(new SoftUniContext()));
            //Console.WriteLine(GetAddressesByTown(new SoftUniContext()));
            //Console.WriteLine(GetEmployee147(new SoftUniContext()));
            //Console.WriteLine(GetDepartmentsWithMoreThan5Employees(new SoftUniContext()));
            //Console.WriteLine(GetLatestProjects(new SoftUniContext()));
            //Console.WriteLine(IncreaseSalaries(new SoftUniContext()));
            //Console.WriteLine(GetEmployeesByFirstNameStartingWithSa(new SoftUniContext()));
            //Console.WriteLine(DeleteProjectById(new SoftUniContext()));
            Console.WriteLine(RemoveTown(new SoftUniContext()));
        }

        //3.Employees Full Information
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            return context.Employees
                .OrderBy(e => e.EmployeeId)
                .Select(employee
                    => $"{employee.FirstName} " +
                       $"{employee.LastName} " +
                       $"{employee.MiddleName} " +
                       $"{employee.JobTitle} " +
                       $"{employee.Salary:f2}")
                .ToString(Environment.NewLine);
        }

        //4.Employees with Salary Over 50 000
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            return context.Employees
                .Where(x => x.Salary > 50000)
                .OrderBy(x => x.FirstName)
                .Select(x => $"{x.FirstName} - {x.Salary:f2}")
                .ToString(Environment.NewLine);
        }

        //5.Employees from Research and Development
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            return context.Employees
                .Where(e => e.Department.Name.Equals("Research and Development"))
                .OrderBy(x => x.Salary)
                .ThenByDescending(e => e.FirstName)
                .Select(e => $"{e.FirstName} {e.LastName} from {e.Department.Name} - ${e.Salary:f2}")
                .ToString(Environment.NewLine);
        }

        //6.Adding a New Address and Updating Employee
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var nakovAddress = new Address() { TownId = 4, AddressText = "Vitoshka 15" };
            var nakov = context.Employees.First(x => x.LastName.Equals("Nakov"));
            nakov.Address = nakovAddress;
            context.SaveChanges();

            return context.Employees
                .Select(x => x.Address)
                .OrderByDescending(x => x.AddressId)
                .Take(10)
                .Select(x => x.AddressText)
                .ToString(Environment.NewLine);
        }

        //7.Employees and Projects
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            return context.Employees
                .Where(e => e.EmployeesProjects
                    .Any(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                .Take(10)
                .Select(x =>
                    $"{x.FirstName} {x.LastName} - Manager: {x.Manager.FirstName} {x.Manager.LastName}{Environment.NewLine}" +
                    @$"{x.EmployeesProjects
                        .Select(ep => @$"--{ep.Project.Name} - {ep.Project.StartDate:M/d/yyyy h:mm:ss tt} - {(ep.Project.EndDate == null ? "not finished" : ep.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt"))}")
                        .ToString(Environment.NewLine)}")
                .ToString(Environment.NewLine);
        }

        //8.Addresses by Town
        public static string GetAddressesByTown(SoftUniContext context)
        {
            return context.Addresses
                .OrderByDescending(a
                    => context.Employees
                        .Count(e => e.AddressId == a.AddressId))
                .ThenBy(a => a.Town.Name)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .Select(a => $"{a.AddressText}, {a.Town.Name} - {a.Employees.Count} employees")
                .ToString(Environment.NewLine);
        }

        //9.Employee 147
        public static string GetEmployee147(SoftUniContext context)
        {
            return context.Employees
                .Where(e => e.EmployeeId == 147)
                .Select(e
                    => $@"{e.FirstName} {e.LastName} - {e.JobTitle}{Environment.NewLine}{
                        e.EmployeesProjects
                            .Select(ep => ep.Project.Name)
                            .OrderBy(p => p)
                            .ToString(Environment.NewLine)}")
                .ToString(Environment.NewLine);
        }

        //10.Departments with More Than 5 Employees
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            return context.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenByDescending(d => d.Name)
                .Select(d => $@"{d.Name} - {d.Manager.FirstName} {d.Manager.LastName}{Environment.NewLine}{
                    d.Employees
                        .OrderBy(e => e.FirstName)
                        .ThenBy(e => e.LastName)
                        .Select(e => $@"{e.FirstName} {e.LastName} - {e.JobTitle}")
                        .ToString(Environment.NewLine)}")
                .ToString(Environment.NewLine);
        }

        //11.Find Latest 10 Projects
        public static string GetLatestProjects(SoftUniContext context)
        {
            return context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name)
                .Select(p => $"{p.Name}{Environment.NewLine}{p.Description}{Environment.NewLine}{p.StartDate:M/d/yyyy h:mm:ss tt}")
                .ToString(Environment.NewLine);
        }

        //12.Increase Salaries
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e =>
                    new[] { "Engineering", "Tool Design", "Marketing", "Information Services" }
                        .Contains(e.Department.Name));
            employees
                .ToList()
                .ForEach(e => e.Salary *= 1.12m);

            context.SaveChanges();

            return employees
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e => $"{e.FirstName} {e.LastName} (${e.Salary:f2})")
                .ToString(Environment.NewLine);
        }

        //13.Find Employees by First Name Starting with "Sa"
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            return context.Employees
                .Where(e => e.FirstName.StartsWith("Sa", StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e => $@"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})")
                .ToString(Environment.NewLine);
        }

        //14.Delete Project by Id
        public static string DeleteProjectById(SoftUniContext context)
        {
            context.EmployeesProjects
                .RemoveRange(context.EmployeesProjects
                    .Where(ep => ep.ProjectId == 2));

            context.Projects
                .Remove(context.Projects
                    .First(p => p.ProjectId == 2));

            context.SaveChanges();

            return context.Projects
                .Take(10)
                .Select(p => $@"{p.Name}")
                .ToString(Environment.NewLine);
        }

        //15.Remove Town
        public static string RemoveTown(SoftUniContext context)
        {
            var addresses = context.Addresses
                .Where(a => a.Town.Name == "Seattle");

            var addresessCount = addresses.Count();

            var employees = context.Employees
                .Where(e
                    => addresses
                        .Select(a => a.AddressId)
                        .Contains(e.AddressId ?? -1));

            employees
                .ToList()
                .ForEach(e => e.AddressId = null);

            context.Addresses
                .RemoveRange(addresses);

            context.Towns
                .Remove(context.Towns
                    .First(t => t.Name == "Seattle"));

            context.SaveChanges();

            return $"{addresessCount} addresses in Seattle were deleted";

        }

        private static string ToString<T>(this IEnumerable<T> collection, string separator)
        {
            return string.Join(separator, collection);
        }
    }
}
