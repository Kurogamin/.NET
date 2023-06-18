using System.ComponentModel.DataAnnotations;

namespace dotnet.Models;

public enum GameGenre
{
    [Display(Name = "Action")]
    Action,

    [Display(Name = "Adventure")]
    Adventure,

    [Display(Name = "Role-Playing Game")]
    RPG,

    [Display(Name = "Simulation")]
    Simulation,

    [Display(Name = "Strategy")]
    Strategy,

    [Display(Name = "Sports")]
    Sports,

    [Display(Name = "Racing")]
    Racing,

    [Display(Name = "Fighting")]
    Fighting,

    [Display(Name = "Shooter")]
    Horror,

    [Display(Name = "Puzzle")]
    Puzzle,

    [Display(Name = "Idle")]
    Idle,

    [Display(Name = "Other")]
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