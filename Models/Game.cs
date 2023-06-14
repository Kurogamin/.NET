using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_MVC_Application.Models;

public enum GameGenre
{
    Action,
    Adventure,
    RPG,
    Simulation,
    Strategy,
    Sports,
    Racing,
    Fighting,
    Horror,
    Puzzle,
    Idle,
    Other
}

public class Game
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public GameGenre Genre { get; set; }
    public string? Description { get; set; }
}
