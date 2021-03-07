using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class Program
{
    static void Main()
    {
        int countToRegister = int.Parse(Console.ReadLine());

        var listOfTeams = new List<Team>();

        for (int i = 0; i < countToRegister; i++)
        {

            string[] token = Console.ReadLine().Split("-");
            string creatorName = token[0];
            string teamName = token[1];


            bool AlreadyUsedTeamName = CheckTeamName(teamName, listOfTeams);
            bool SameCreator = CheckCreator(creatorName, listOfTeams);

            if (AlreadyUsedTeamName)
            {
                Console.WriteLine($"Team {teamName} was already created!");
                continue;
            }

            if (SameCreator)
            {
                Console.WriteLine($"{creatorName} cannot create another team!");
                continue;
            }

            listOfTeams.Add(new Team(teamName, creatorName));
            Console.WriteLine($"Team {teamName} has been created by {creatorName}!");
        }

        string input;

        while ((input = Console.ReadLine()) != "end of assignment")
        {
            string[] token = input.Split("->");
            string memberName = token[0];
            string teamName = token[1];

            bool teamExists = TeamExists(teamName, listOfTeams);
            bool joinFail = JoinFail(memberName, listOfTeams, teamName);


            if (teamExists)
            {
                foreach (var team in listOfTeams)
                {
                    if (team.Name == teamName && !joinFail)
                    {
                        team.Members.Add(memberName);
                        break;
                    }
                }
            }
            if (!teamExists)
            {
                Console.WriteLine($"Team {teamName} does not exist!");
                continue;
            }

            if (joinFail)
            {
                Console.WriteLine($"Member {memberName} cannot join team {teamName}!");
                continue;
            }
        }
        var teamsLeft = listOfTeams
            .Where(x => x.Members.Count > 0)
            .OrderBy(x => x.Members.Count).Reverse()
            //.OrderBy(x => x.Name)
            .ToList();

        var teamsToDisband = listOfTeams
            .Where(x => x.Members.Count == 0)
            .OrderBy(x => x.Name)
            .ToList();

        foreach (var team in teamsLeft)
        {
            Console.WriteLine(team.Name);
            Console.WriteLine("- " + team.Creator);

            var sortedMembers = team.Members.OrderBy(m => m).ToList();
            foreach (var member in sortedMembers)
            {

                Console.WriteLine("-- " + member);
            }
        }

        Console.WriteLine("Teams to disband:");

        foreach (var team in teamsToDisband)
        {
            Console.WriteLine(team.Name);
        }
    }

    static bool CheckCreator(string creatorName, List<Team> list)
    {
        foreach (var team in list)
            if (team.Creator == creatorName)
            {
                return true;
            }
        return false;
    }
    static bool CheckTeamName(string teamName, List<Team> list)
    {
        foreach (var team in list)
            if (team.Name == teamName)
            {
                return true;
            }
        return false;
    }

    static bool JoinFail(string memberName, List<Team> list, string teamName)
    {
        foreach (var team in list)
        {
            if (team.Name == teamName)
            {
                if (team.Creator == memberName)
                {
                    return true;
                }
            }
            foreach (var member in team.Members)
            {
                if (member == memberName)
                {
                    return true;
                }
            }
        }
        return false;
    }

    static bool TeamExists(string teamName, List<Team> list)
    {
        foreach (var team in list)
        {
            if (team.Name == teamName)
            {
                return true;
            }
        }
        return false;
    }
}

class Team
{
    public Team(string name, string creatorName)
    {
        this.Name = name;
        this.Creator = creatorName;
        this.Members = new List<string>();
    }

    public Team()
    {

    }
    public string Name { get; set; }
    public string Creator { get; set; }
    public List<string> Members { get; set; }

}