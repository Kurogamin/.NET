using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dotnet.Models.ViewModels;

public class GameViewModel
{
    public Game Game { get; set; }

    public GameViewModel()
    {
        Game = new Game();
    }

    [ValidateNever]
    public IEnumerable<SelectListItem>? StudioList { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem>? GenreList { get; set; }
}