using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace _06._Remove_Villain
{
    static class Program
    {
        static void Main(string[] args)
        {
            const string querySelectVillainName = @"SELECT Name FROM Villains WHERE Id = @villainId";

            const string queryDeleteVillainMinionRelationship = @"DELETE FROM MinionsVillains
                                                                  WHERE VillainId = @villainId";
            const string queryDeleteVillain = @"DELETE FROM Villains
                                                WHERE Id = @villainId";

            var villainId = int.Parse(Console.ReadLine()!);

            using var conn = new SqlConnection("server=.;database=minionsdb;integrated security=true");
            conn.Open();

            using var command = conn.CreateCommand();
            command.Transaction = conn.BeginTransaction();

            command.CommandText = querySelectVillainName;
            var dbVillainId = command.Parameters.Add("@villainId", SqlDbType.Int);
            dbVillainId.Value = villainId;

            if(!(command.ExecuteScalar() is string villainName))
            {
                Console.WriteLine("No such villain was found.");
                return;
            }

            command.CommandText = queryDeleteVillainMinionRelationship;
            int releasedMinions = command.ExecuteNonQuery();

            command.CommandText = queryDeleteVillain;
            command.ExecuteNonQuery();

            Console.WriteLine($"{villainName} was deleted.");
            Console.WriteLine($"{releasedMinions} minions were released.");

            command.Transaction.Commit();
        }
    }
}
