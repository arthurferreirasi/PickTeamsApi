using Microsoft.AspNetCore.Mvc;

public class Player {
    [BindProperty]
    public string name { get; set; }
    [BindProperty]
    public bool isSeed { get; set; } = false;
}