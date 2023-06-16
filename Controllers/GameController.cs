using ASP.NET_Core_MVC_Application.Data;
using ASP.NET_Core_MVC_Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_MVC_Application.Controllers;
public class GameController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public GameController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IActionResult Index()
    {
        IEnumerable<Game> games = _dbContext.Games;
        return View(games);
    }
}
