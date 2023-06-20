﻿using dotnet.DataAccess;
using dotnet.DataAccess.Data;
using dotnet.DataAccess.Repository.IRepository;
using dotnet.Models;
using dotnet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dotnet.Areas.Admin.Controllers;

[Area("Admin")]
public class GameController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public GameController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Game> games = _unitOfWork.GameRepository.GetAll();

        return View(games);
    }

    public IActionResult Create()
    {
        GameViewModel gameViewModel = new GameViewModel
        {
            Game = new Game(),
            StudioList = _unitOfWork.StudioRepository.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            })
        };

        return View(gameViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(GameViewModel newGameViewModel)
    {
        if (!ModelState.IsValid)
        {
            newGameViewModel.StudioList = _unitOfWork.StudioRepository.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View(newGameViewModel);
        }

        _unitOfWork.GameRepository.Add(newGameViewModel.Game);
        _unitOfWork.Save();

        TempData["Success"] = "The game has been added successfully!";

        return RedirectToAction("Index");
    }

    public IActionResult Edit(int? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        var gameFromDatabase = _unitOfWork.GameRepository.Get(x => x.Id == id);

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

        _unitOfWork.GameRepository.Update(game);
        _unitOfWork.Save();

        TempData["Success"] = "The game has been updated successfully!";

        return RedirectToAction("Index");
    }

    public IActionResult Remove(int? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        var gameFromDatabase = _unitOfWork.GameRepository.Get(x => x.Id == id);

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
        var gameFromDatabase = _unitOfWork.GameRepository.Get(x => x.Id == id);

        if (gameFromDatabase is null)
        {
            return NotFound();
        }

        _unitOfWork.GameRepository.Remove(gameFromDatabase);
        _unitOfWork.Save();

        TempData["Success"] = "The game has been removed successfully!";

        return RedirectToAction("Index");
    }
}