public interface IPickTeamsService{
    List<Player> ShuffleList(List<Player> players);
    TeamsList GetTeams(List<Player> players, int quantity);
    List<Player> GetSeedPlayers(List<Player> list);
    TeamsList GetTeamsWithSeedPlayers(List<Player> players, int quantity);
    TeamsList AddMissPlayers(List<Player> players, int quantity, TeamsList teamsList);
}

public class PickTeamsService : IPickTeamsService
{
    private TeamsList teamsList;
    public List<Player> ShuffleList(List<Player> players)
    {
        var rdn = new Random();
        return players.OrderBy(x => rdn.Next()).ToList();
    }
    public TeamsList GetTeams(List<Player> players, int quantity)
    {
        this.teamsList = new TeamsList();
        int indexName = 1;
        var team = new Team();
        for (int i = 0; i < players.Count; i++)
        {
            team.players.Add(players[i]);

            if (team.players.Count == quantity || i == players.Count - 1)
            {
                team.teamName = "Time " + indexName;
                this.teamsList.teams.Add(team);
                indexName++;
                team = new Team();
            }
        }

        return this.teamsList;
    }

    public List<Player> GetSeedPlayers(List<Player> list)
    {
        return list.Where(x => x.isSeed == true).ToList();
    }

    public TeamsList GetTeamsWithSeedPlayers(List<Player> players, int quantity)
    {
        int countTeams = players.Count % quantity == 0 ? players.Count / quantity : players.Count / quantity + 1;
        int index = 1;
        var team = new Team();
        this.teamsList = new TeamsList();
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
                    this.teamsList.teams.Add(team);
                    index++;
                    team = new Team();
                }
                else
                {
                    allTeamsSeeded = true;
                    index = 0;
                    this.teamsList.teams[index].players.Add(seedPlayers[i]);
                    index++;
                }
            }
            else
            {
                 this.teamsList.teams[index].players.Add(seedPlayers[i]);
                index++;
            }
        }

        this.teamsList = AddMissPlayers(players, quantity, this.teamsList);

        return this.teamsList;
    }

    public TeamsList AddMissPlayers(List<Player> players, int quantity, TeamsList teamsList)
    {
        var index = 0;
        for (int i = 0; i < players.Count; i++)
        {
             this.teamsList.teams[index].players.Add(players[i]);

            if ( this.teamsList.teams[index].players.Count == quantity || i == players.Count - 1)
            {
                index++;
            }
        }

        return this.teamsList;
    }
}