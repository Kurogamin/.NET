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

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Game newGame)
    {
        if (!ModelState.IsValid)
        {
            return View(newGame);
        }

        _dbContext.Games.Add(newGame);
        _dbContext.SaveChanges();

        TempData["Success"] = "The game has been added successfully!";


        return RedirectToAction("Index");
    }

    public IActionResult Edit(int? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        var gameFromDatabase = _dbContext.Games.Find(id);

        if (gameFromDatabase is null)
        {
            return NotFound();
        }

        return View(gameFromDatabase);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Game game)
    {
        if (!ModelState.IsValid)
        {
            return View(game);
        }

        _dbContext.Games.Update(game);
        _dbContext.SaveChanges();

        TempData["Success"] = "The game has been updated successfully!";

        return RedirectToAction("Index");
    }

    public IActionResult Remove(int? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        var gameFromDatabase = _dbContext.Games.Find(id);

        if (gameFromDatabase is null)
        {
            return NotFound();
        }

        return View(gameFromDatabase);
    }

    [HttpPost, ActionName("Remove")]
    [ValidateAntiForgeryToken]
    public IActionResult RemovePOST(int? id)
    {
        var gameFromDatabase = _dbContext.Games.Find(id);

        if (gameFromDatabase is null)
        {
            return NotFound();
        }

        _dbContext.Games.Remove(gameFromDatabase);
        _dbContext.SaveChanges();

        TempData["Success"] = "The game has been removed successfully!";


        return RedirectToAction("Index");
    }
}