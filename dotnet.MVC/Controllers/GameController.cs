using dotnet.DataAccess;
using dotnet.DataAccess.Data;
using dotnet.DataAccess.Repository.IRepository;
using dotnet.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers;

public class GameController : Controller
{
    private readonly IGameRepository _gameRepository;

    public GameController(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public IActionResult Index()
    {
        IEnumerable<Game> games = _gameRepository.GetAll();
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

        _gameRepository.Add(newGame);
        _gameRepository.SaveChanges();

        TempData["Success"] = "The game has been added successfully!";

        return RedirectToAction("Index");
    }

    public IActionResult Edit(int? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        var gameFromDatabase = _gameRepository.Get(x => x.Id == id);

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

        _gameRepository.Update(game);
        _gameRepository.SaveChanges();

        TempData["Success"] = "The game has been updated successfully!";

        return RedirectToAction("Index");
    }

    public IActionResult Remove(int? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        var gameFromDatabase = _gameRepository.Get(x => x.Id == id);

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
        var gameFromDatabase = _gameRepository.Get(x => x.Id == id);

        if (gameFromDatabase is null)
        {
            return NotFound();
        }

        _gameRepository.Remove(gameFromDatabase);
        _gameRepository.SaveChanges();

        TempData["Success"] = "The game has been removed successfully!";

        return RedirectToAction("Index");
    }
}