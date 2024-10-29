using Microsoft.AspNetCore.Mvc;

public class Team {

    [BindProperty]
    public List<Player> players { get; set; }

    [BindProperty]
    public string teamName { get; set;}

    public Team()
    {
        players = new List<Player>();
    }
}

public class TeamsList {
    public List<Team> teams { get; set; }

    public TeamsList(){
        teams = new List<Team>();
    }
}