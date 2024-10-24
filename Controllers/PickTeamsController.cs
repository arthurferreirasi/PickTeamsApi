using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PickTeamsController : ControllerBase
{

    [HttpPost, Route("pickTeams")]
    public IActionResult PickTeams([FromBody] List<string> listPlayers, int quantity)
    {
        var players = shuffleList(listPlayers);
        var teams = GetTeams(players, quantity);
        return Ok(teams);
    }

    private List<string> shuffleList(List<string> players)
    {
        var random = new Random();
        var list = new List<string>(players);
        for (int i = players.Count - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            string temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        return list;
    }

    private List<Team> GetTeams(List<string> players, int quantity)
    {
        var teamsList = new List<Team>();
        int indexName = 1;
        var team = new Team();

        for (int i = 0; i < players.Count; i++)
        {
            team.players.Add(players[i]);

            if (team.players.Count == quantity || i == players.Count -1 ){
                team.teamName = "Time " + indexName;
                teamsList.Add(team);
                indexName++;
                team = new Team();
            }
        }

        return teamsList;
    }
}