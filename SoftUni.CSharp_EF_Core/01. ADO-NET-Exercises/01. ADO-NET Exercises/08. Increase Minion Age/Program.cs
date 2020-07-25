using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace _08._Increase_Minion_Age
{
    static class Program
    {
        static void Main(string[] args)
        {
            const  string queryUpdateAge = @"UPDATE Minions
                                                SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
                                            WHERE Id = @Id";

            const string queryGetMinions = @"SELECT Name, Age FROM Minions";

            var minionsIds = Console.ReadLine()!
                .Split();

            using var conn = new SqlConnection("server=.;database=minionsdb;integrated security=true;");
            conn.Open();

            using var command = conn.CreateCommand();
            command.Transaction = conn.BeginTransaction();

            command.CommandText = queryUpdateAge;
            var dbParam = command.Parameters.Add("@Id", SqlDbType.Int);
            int updates = 0;
            foreach (var id in minionsIds)
            {
                dbParam.Value = id;
                updates += command.ExecuteNonQuery();
            }

            if (updates != minionsIds.Length)
            {
                command.Transaction.Rollback();
                return;
            }

            command.Transaction.Commit();

            command.CommandText = queryGetMinions;
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader["Name"]} {reader["Age"]}");
            }
        }
    }
}
