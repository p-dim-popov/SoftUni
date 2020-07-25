using System;
using System.Linq;

namespace P01_HospitalDatabase
{
    using Microsoft.EntityFrameworkCore;
    using Data;

    public class Program
    {
        static void Main(string[] args)
        {
            var context = new HospitalContext();
            context.Database.Migrate();

            // while (true)
            // {
            //     var input = Console.ReadLine()!.Split(" ");
            //     var command = input[0];
            //     var arg = input[1];
            //     switch (command.ToLower())
            //     {
            //         case "add":
            //             break;
            //         case "view":
            //             switch (arg.ToLower())
            //             {
            //                 case "patient":
            //                     var patientsDiagnoses = context.Patients
            //                         .Where(p => new[] {p.FirstName, p.LastName}
            //                             .Any(n => n == arg))
            //                         .Select(p => $"{p.FirstName} {p.LastName}{Environment.NewLine}" +
            //                                      $"{string.Join(Environment.NewLine, p.Diagnoses.Select(d => d.Name))}");
            //                     Console.WriteLine(patientsDiagnoses);
            //                     break;
            //                 case "diagnose":
            //                     break;
            //                 default:
            //                     break;
            //             }
            //             break;
            //         case "edit":
            //             break;
            //         case "delete":
            //             break;
            //         default:
            //             Console.WriteLine("Enter again");
            //             break;
            //     }
            // }
        }
    }
}