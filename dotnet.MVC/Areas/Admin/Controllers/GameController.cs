﻿using dotnet.DataAccess;
using dotnet.DataAccess.Data;
using dotnet.DataAccess.Repository.IRepository;
using dotnet.Models;
using dotnet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace dotnet.Areas.Admin.Controllers;

[Area("Admin")]
public class GameController : Controller
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IWebHostEnvironment _webHostEnvironment;

	public GameController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
	{
		_unitOfWork = unitOfWork;
		_webHostEnvironment = webHostEnvironment;
	}

	private void AddImageToGameViewModel(GameViewModel gameViewModel, IFormFile imageFile)
	{
		string webRootPath = _webHostEnvironment.WebRootPath;

		if (!string.IsNullOrEmpty(gameViewModel.Game.ImageUrl))
		{
			RemoveOldImage(gameViewModel, webRootPath);
		}

		string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
		string gameImagesPath = Path.Combine(webRootPath, @"images\game");

		using (var fileStream = new FileStream(Path.Combine(gameImagesPath, fileName), FileMode.Create))
		{
			imageFile.CopyTo(fileStream);
		}

		gameViewModel.Game.ImageUrl = $@"\images\game\{fileName}";
	}

	private static void RemoveOldImage(GameViewModel gameViewModel, string webRootPath)
	{
		string oldImagePath = Path.Combine(webRootPath, gameViewModel.Game.ImageUrl.TrimStart('\\'));

		if (System.IO.File.Exists(oldImagePath))
		{
			System.IO.File.Delete(oldImagePath);
		}
	}

	private GameViewModel CreateNewViewModel()
	{
		return new GameViewModel
		{
			Game = new Game(),
			StudioList = _unitOfWork.StudioRepository.GetAll().Select(x => new SelectListItem
			{
				Text = x.Name,
				Value = x.Id.ToString()
			}),

			GenreList = _unitOfWork.GenreRepository.GetAll().Select(x => new SelectListItem
			{
				Text = x.Name,
				Value = x.Id.ToString()
			})
		};
	}

	private void PopulateSelectLists(GameViewModel gameViewModel)
	{
		gameViewModel.StudioList = _unitOfWork.StudioRepository.GetAll().Select(x => new SelectListItem
		{
			Text = x.Name,
			Value = x.Id.ToString()
		});

		gameViewModel.GenreList = _unitOfWork.GenreRepository.GetAll().Select(x => new SelectListItem
		{
			Text = x.Name,
			Value = x.Id.ToString()
		});
	}

	public IActionResult Index()
	{
		IEnumerable<Game> games = _unitOfWork.GameRepository.GetAll(includeProperties: "Studio,Genre");

		return View(games);
	}

	public IActionResult Upsert(int? id)
	{
		GameViewModel gameViewModel = CreateNewViewModel();

		if (id is null || id == 0)
		{
			return View(gameViewModel);
		}

		gameViewModel.Game = _unitOfWork.GameRepository.Get(x => x.Id == id);
		return View(gameViewModel);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Upsert(GameViewModel gameViewModel, IFormFile? imageFile)
	{
		if (!ModelState.IsValid)
		{
			PopulateSelectLists(gameViewModel);

			return View(gameViewModel);
		}

		if (imageFile is not null)
		{
			AddImageToGameViewModel(gameViewModel, imageFile);
		}

		if (gameViewModel.Game.Id == 0)
		{
			_unitOfWork.GameRepository.Add(gameViewModel.Game);
		}
		else
		{
			_unitOfWork.GameRepository.Update(gameViewModel.Game);
		}

		_unitOfWork.Save();

		TempData["Success"] = "The game has been added successfully!";

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

	[HttpGet]
	public IActionResult GetAll()
	{
		List<Game> gamesList = _unitOfWork.GameRepository.GetAll(includeProperties: "Studio,Genre").ToList();
		return Json(new { data = gamesList });
	}
}