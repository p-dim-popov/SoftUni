using System;
using Microsoft.Data.SqlClient;

namespace _02._Villain_Names
{
    static class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=.; Database=MinionsDB; Integrated security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();

                string selectionCommandString = "SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  \n"
                                                + "    FROM Villains AS v \n"
                                                + "    JOIN MinionsVillains AS mv ON v.Id = mv.VillainId \n"
                                                + "GROUP BY v.Id, v.Name \n"
                                                + "  HAVING COUNT(mv.VillainId) > 3 \n"
                                                + "ORDER BY COUNT(mv.VillainId) DESC";

                SqlCommand command = new SqlCommand(selectionCommandString, connection);
                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} - {reader["MinionsCount"]}");
                    }
                }
            }
        }
    }
}
