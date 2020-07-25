using System;
using Microsoft.Data.SqlClient;

namespace _03._Minion_Names
{
    static class Program
    {
        static void Main(string[] args)
        {
            const string queryGetVillainById = @"SELECT Name FROM Villains WHERE Id = @Id";
            const string queryGetMinionsOfVillain = @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                                             m.Name, 
                                                             m.Age
                                                    FROM MinionsVillains AS mv
                                                    JOIN Minions As m ON mv.MinionId = m.Id
                                                       WHERE mv.VillainId = @Id
                                                    ORDER BY m.Name";

            if (!int.TryParse(Console.ReadLine(), out int villainId))
            {
                Console.WriteLine("Wrong Id format");
                return;
            }

            using var sqlConnection = new SqlConnection("server=.;database=MinionsDB;integrated security=true;");
            sqlConnection.Open();
            
            var sqlCommand = new SqlCommand(queryGetVillainById, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", villainId);

            if (!(sqlCommand.ExecuteScalar() is string villainName))
            {
                Console.WriteLine($"No villain with ID {villainId}");
                return;
            }
            Console.WriteLine($"Villain: {villainName}");

            sqlCommand = new SqlCommand(queryGetMinionsOfVillain, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", villainId);
            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine("(no minions)");
                return;
            }

            while (reader.Read())
            {
                Console.WriteLine($"{reader["RowNum"]}. {reader["Name"]} {reader["Age"]}");
            }
        }
    }
}
