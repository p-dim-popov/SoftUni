using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace _07._Print_All_Minion_Names
{
    static class Program
    {
        static void Main(string[] args)
        {
            IList<string> minionNames;
            using var conn = new SqlConnection("server=.;database=minionsdb;integrated security=true;");
            conn.Open();

            using (var command = conn.CreateCommand())
            {
                command.CommandText = "SELECT Name FROM Minions";
                using var reader = command.ExecuteReader();
                minionNames = new List<string>(reader
                    .Select(r => r["Name"] as string));

            }

            var minionsHalfCount = minionNames.Count / 2 + 1;
            if (minionNames.Count % 2 == 0)
            {
                for (int i = 0; i < minionsHalfCount; i++)
                {
                    Console.WriteLine(minionNames[i]);
                    Console.WriteLine(minionNames[minionNames.Count - i - 1]);
                }
            }
            else
            {
                for (int i = 0; i < minionsHalfCount - 1; i++)
                {
                    Console.WriteLine(minionNames[i]);
                    Console.WriteLine(minionNames[minionNames.Count - i - 1]);
                }
                Console.WriteLine(minionNames[minionNames.Count / 2]);
            }

        }

        private static IEnumerable<T> Select<T>(this IDataReader reader, Func<IDataReader, T> projection)
        {
            while (reader.Read()) yield return projection(reader);
        }
    }
}
