using System;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Data.SqlClient;

namespace _09._Increase_Age_Stored_Procedure
{
    static class Program
    {
        static void Main(string[] args)
        {
            const string queryGetMinion = @"SELECT Name, Age FROM Minions WHERE Id = @Id";

            Console.Write("Should I create/alter the stored procedure? [y/N] ");
            var decision = Console.ReadLine();
            if (decision == "y" || decision == "Y")
            {
                CreateStoredProcedureIncreaseMinionsAge();
                Console.WriteLine("Creation successfull!");
            }

            var minionId = int.Parse(Console.ReadLine()!);

            using var conn = new SqlConnection("server=.;database=minionsdb;integrated security=true;");
            conn.Open();

            using var command = conn.CreateCommand();
            command.CommandText = "EXEC dbo.usp_GetOlder @Id";
            command.Parameters.AddWithValue("@Id", minionId);
            command.ExecuteNonQuery();

            command.CommandText = queryGetMinion;
            using var reader = command.ExecuteReader();
            reader.Read();
            Console.WriteLine($"{reader["Name"]} - {reader["Age"]}");
        }

        private static int CreateStoredProcedureIncreaseMinionsAge()
        {
            const string queryCreateProcedure = @"CREATE OR ALTER PROC [usp_GetOlder] @id INT
                                                    AS
                                                        UPDATE Minions
                                                            SET Age += 1
                                                        WHERE Id = @id";
            using var conn = new SqlConnection("server=.;database=minionsdb;integrated security=true;");
            conn.Open();

            using var command = conn.CreateCommand();
            command.CommandText = queryCreateProcedure;

            return command.ExecuteNonQuery();
        }
    }
}
