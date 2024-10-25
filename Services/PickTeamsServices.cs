public interface IPickTeamsService{
    List<Player> ShuffleList(List<Player> players);
    List<Team> GetTeams(List<Player> players, int quantity);
    List<Player> GetSeedPlayers(List<Player> list);
    List<Team> GetTeamsWithSeedPlayers(List<Player> players, int quantity);
    List<Team> AddMissPlayers(List<Player> players, int quantity, List<Team> teamsList);
}

public class PickTeamsService : IPickTeamsService
{
    public List<Player> ShuffleList(List<Player> players)
    {
        var rdn = new Random();
        return players.OrderBy(x => rdn.Next()).ToList();
    }
    public List<Team> GetTeams(List<Player> players, int quantity)
    {
        var teamsList = new List<Team>();
        int indexName = 1;
        var team = new Team();
        for (int i = 0; i < players.Count; i++)
        {
            team.players.Add(players[i]);

            if (team.players.Count == quantity || i == players.Count - 1)
            {
                team.teamName = "Time " + indexName;
                teamsList.Add(team);
                indexName++;
                team = new Team();
            }
        }

        return teamsList;
    }

    public List<Player> GetSeedPlayers(List<Player> list)
    {
        return list.Where(x => x.isSeed == true).ToList();
    }

    public List<Team> GetTeamsWithSeedPlayers(List<Player> players, int quantity)
    {
        int countTeams = players.Count % quantity == 0 ? players.Count / quantity : players.Count / quantity + 1;
        int index = 1;
        var teamsList = new List<Team>();
        var team = new Team();
        var allTeamsSeeded = false;

        var seedPlayers = GetSeedPlayers(players);
        players.RemoveAll(x => x.isSeed == true);

        for (int i = 0; i < seedPlayers.Count; i++)
        {
            if (!allTeamsSeeded)
            {
                if (index <= countTeams)
                {
                    team.teamName = "Time " + index;
                    team.players.Add(seedPlayers[i]);
                    teamsList.Add(team);
                    index++;
                    team = new Team();
                }
                else
                {
                    allTeamsSeeded = true;
                    index = 0;
                    teamsList[index].players.Add(seedPlayers[i]);
                    index++;
                }
            }
            else
            {
                teamsList[index].players.Add(seedPlayers[i]);
                index++;
            }
        }

        teamsList = AddMissPlayers(players, quantity, teamsList);

        return teamsList;
    }

    public List<Team> AddMissPlayers(List<Player> players, int quantity, List<Team> teamsList)
    {
        var index = 0;
        for (int i = 0; i < players.Count; i++)
        {
            teamsList[index].players.Add(players[i]);

            if (teamsList[index].players.Count == quantity || i == players.Count - 1)
            {
                index++;
            }
        }

        return teamsList;
    }
}