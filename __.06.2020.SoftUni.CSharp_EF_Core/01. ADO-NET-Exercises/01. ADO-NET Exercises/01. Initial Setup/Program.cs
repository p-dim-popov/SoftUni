using System;
using Microsoft.Data.SqlClient;

namespace _01._Initial_Setup
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("WARNING! THIS WILL DELETE DATABASE [MinionsDB]! PLEASE CLOSE ALL EXISTING CONNECTIONS! CONTINUE? [y/N] ");
            var decision = Console.ReadLine();
            if (decision != "y" && decision != "Y") return;

            string databaseName = "MinionsDB";
            string connectionString = "Server=.; Database=master; Integrated security=True";
            var connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();

                string createDbCommandString = $"DROP DATABASE IF EXISTS {databaseName}; CREATE DATABASE {databaseName}";
                SqlCommand commandCreateDatabase = new SqlCommand(createDbCommandString, connection);
                int result = commandCreateDatabase.ExecuteNonQuery();
            }

            connectionString = $"Server=.; Database={databaseName}; Integrated security=true";
            connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                string createTablesCommandString =
                    "CREATE TABLE [Countries] (Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50))\n"
                    + "CREATE TABLE [Towns] (Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50), CountryCode INT FOREIGN KEY REFERENCES Countries(Id))\n"
                    + "CREATE TABLE [Minions](Id INT PRIMARY KEY IDENTITY,Name VARCHAR(30), Age INT, TownId INT FOREIGN KEY REFERENCES Towns(Id))\n"
                    + "CREATE TABLE [EvilnessFactors] (Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50))\n"
                    + "CREATE TABLE [Villains] (Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id))\n"
                    + "CREATE TABLE [MinionsVillains] (MinionId INT FOREIGN KEY REFERENCES Minions(Id),VillainId INT FOREIGN KEY REFERENCES Villains(Id),CONSTRAINT PK_MinionsVillains PRIMARY KEY (MinionId, VillainId))";

                SqlCommand commandCreateTables = new SqlCommand(createTablesCommandString, connection);
                commandCreateTables.ExecuteNonQuery();

                string insertDataCommandString =
                    "INSERT INTO Countries ([Name]) VALUES ('Bulgaria'),('England'),('Cyprus'),('Germany'),('Norway')\n"
                     + "INSERT INTO Towns ([Name], CountryCode) VALUES ('Plovdiv', 1),('Varna', 1),('Burgas', 1),('Sofia', 1),('London', 2),('Southampton', 2),('Bath', 2),('Liverpool', 2),('Berlin', 3),('Frankfurt', 3),('Oslo', 4)\n"
                     + "INSERT INTO Minions (Name,Age, TownId) VALUES('Bob', 42, 3),('Kevin', 1, 1),('Bob ', 32, 6),('Simon', 45, 3),('Cathleen', 11, 2),('Carry ', 50, 10),('Becky', 125, 5),('Mars', 21, 1),('Misho', 5, 10),('Zoe', 125, 5),('Json', 21, 1)\n"
                     + "INSERT INTO EvilnessFactors (Name) VALUES ('Super good'),('Good'),('Bad'), ('Evil'),('Super evil')\n"
                     + "INSERT INTO Villains (Name, EvilnessFactorId) VALUES ('Gru',2),('Victor',1),('Jilly',3),('Miro',4),('Rosen',5),('Dimityr',1),('Dobromir',2)\n"
                     + "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (4,2),(1,1),(5,7),(3,5),(2,6),(11,5),(8,4),(9,7),(7,1),(1,3),(7,3),(5,3),(4,3),(1,2),(2,1),(2,7)";

                SqlCommand commandInsertData = new SqlCommand(insertDataCommandString, connection);
                commandInsertData.ExecuteNonQuery();
            }
        }
    }
}
