using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace _04.Add_Minion
{
    static class Program
    {
        static void Main(string[] args)
        {
            const string queryGetVillainIdByName = "SELECT Id FROM Villains WHERE Name = @Name";
            const string queryGetMinionIdByName = "SELECT Id FROM Minions WHERE Name = @Name";
            const string queryAssignMinionToVilain = "INSERT INTO MinionsVillains(MinionId, VillainId) VALUES(@villainId, @minionId)";
            const string queryAddVillain = "INSERT INTO Villains(Name, EvilnessFactorId) OUTPUT [INSERTED].Id VALUES(@villainName, 4);";
            const string queryAddMinion = "INSERT INTO Minions(Name, Age, TownId) OUTPUT [INSERTED].Id VALUES(@nam, @age, @townId);";
            const string queryAddTown = "INSERT INTO Towns(Name) OUTPUT [INSERTED].Id VALUES(@townName);";
            const string queryGetTownIdByName = "SELECT Id FROM Towns WHERE Name = @townName";

            var splittedInput = Console.ReadLine()!.Split();
            var (minionName, minionAge, townName) = (splittedInput[1], int.Parse(splittedInput[2]), splittedInput[3]);
            splittedInput = Console.ReadLine()!.Split();
            var villainName = splittedInput[1];

            using var connection = new SqlConnection("server=.;database=minionsdb;integrated security=true");
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            using var command = new SqlCommand(queryGetTownIdByName, connection, transaction);

            try
            {
                if (!(command.GetRecordColumn(queryGetTownIdByName, ("@townName", townName)) is int townId))
                {
                    townId = command.AddRecord(queryAddTown, ("@townName", townName));
                    Console.WriteLine($"Town {townName} was added to the database.");
                }

                if (!(command.GetRecordColumn(queryGetVillainIdByName, ("@Name", villainName)) is int villainId))
                {
                    villainId = command.AddRecord(queryAddVillain, ("@villainName", villainName));
                    Console.WriteLine($"Villain {villainName} was added to the database.");
                }

                int minionId = command.AddRecord(queryAddMinion, ("@nam", minionName), ("@age", minionAge), ("@townId", townId));

                command.CommandText = queryAssignMinionToVilain;
                command.Parameters.Clear();
                var dbVillainId = command.Parameters.Add("@villainId", SqlDbType.Int);
                dbVillainId.Value = villainId;
                var dbMinionId = command.Parameters.Add("@minionId", SqlDbType.Int);
                dbMinionId.Value = minionId;
                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");

                command.Transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Console.WriteLine(e);
            }
        }

        private static int AddRecord(this SqlCommand command, string queryAdd, params (string, object)[] argsTuples)
        {
            command.CommandText = queryAdd;
            command.Parameters.Clear();
            foreach (var argsTuple in argsTuples)
            {
                command.Parameters.AddWithValue(argsTuple.Item1, argsTuple.Item2);
            }

            return (int)command.ExecuteScalar();
        }

        private static object GetRecordColumn(this SqlCommand command, string queryGet,
            params (string, object)[] argsTupples)
        {
            command.CommandText = queryGet;
            command.Parameters.Clear();
            foreach (var argsTupple in argsTupples)
            {
                command.Parameters.AddWithValue(argsTupple.Item1, argsTupple.Item2);
            }

            return command.ExecuteScalar();
        }
    }
}