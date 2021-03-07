using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace _05.Change_Town_Names_Casing
{
    static class Program
    {
        static void Main(string[] args)
        {
            const string queryUpdateTownNamesForCountry = @"UPDATE Towns
                                                                SET Name = UPPER(Name)
                                                            WHERE CountryCode = (SELECT c.Id FROM Countries AS c 
                                                                                 WHERE c.Name = @countryName)";

            const string querySelectTownNamesForCountry = @"SELECT t.Name
                                                                FROM Towns as t
                                                            JOIN Countries AS c 
                                                                ON c.Id = t.CountryCode
                                                            WHERE c.Name = @countryName";

            var countryName = Console.ReadLine();

            using var conn = new SqlConnection("server=.;database=minionsdb;integrated security=true");
            conn.Open();

            using var command = conn.CreateCommand();
            command.CommandText = queryUpdateTownNamesForCountry;
            command.Parameters.AddWithValue("@countryName", countryName);

            int affectedRows = command.ExecuteNonQuery();
            Console.WriteLine($"{(affectedRows < 1 ? "No" : affectedRows.ToString())} town names were affected.");

            if (affectedRows < 1) return;

            command.CommandText = querySelectTownNamesForCountry;
            using var reader = command.ExecuteReader();
            var townNames = reader
                .Select(r => r["Name"] as string)
                .ToList();
            Console.WriteLine($"[{string.Join(", ", townNames)}]");
        }

        private static IEnumerable<T> Select<T>(this IDataReader reader, Func<IDataReader, T> projection)
        {
            while (reader.Read())
            {
                yield return projection(reader);
            }
        }
    }
}
