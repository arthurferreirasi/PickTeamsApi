using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PickTeamsController : ControllerBase
{
    private readonly IPickTeamsService _pickTeamsService;
    private TeamsList teamsList;

    public PickTeamsController(IPickTeamsService service){
        _pickTeamsService = service;
    }

    [HttpPost]
    public IActionResult PickTeams([FromBody] List<Player> listPlayers, int quantityPerTeam)
    {
        var players = _pickTeamsService.ShuffleList(listPlayers);
        this.teamsList = _pickTeamsService.GetTeams(players, quantityPerTeam);
        return Ok(this.teamsList);
    }

    [HttpPost]
    [Route("PickTeamsSeedPlayers")]
    public IActionResult PickTeamsSeedPlayers([FromBody] List<Player> listPlayers, int quantityPerTeam)
    {
        var players = _pickTeamsService.ShuffleList(listPlayers);
        var teams = _pickTeamsService.GetTeamsWithSeedPlayers(players, quantityPerTeam);
        return Ok(teams);
    }

}