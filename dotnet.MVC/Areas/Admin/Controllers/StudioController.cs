using dotnet.DataAccess.Repository.IRepository;
using dotnet.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Areas.Admin.Controllers;

[Area("Admin")]
public class StudioController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public StudioController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Studio> studios = _unitOfWork.StudioRepository.GetAll();
        return View(studios);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Studio newStudio)
    {
        if (!ModelState.IsValid)
        {
            return View(newStudio);
        }

        _unitOfWork.StudioRepository.Add(newStudio);
        _unitOfWork.Save();

        TempData["Success"] = "The Studio has been added successfully!";

        return RedirectToAction("Index");
    }

    public IActionResult Edit(int? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        var studioFromDatabase = _unitOfWork.StudioRepository.Get(x => x.Id == id);

        if (studioFromDatabase is null)
        {
            return NotFound();
        }

        return View(studioFromDatabase);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Studio studio)
    {
        if (!ModelState.IsValid)
        {
            return View(studio);
        }

        _unitOfWork.StudioRepository.Update(studio);
        _unitOfWork.Save();

        TempData["Success"] = "The Studio has been updated successfully!";

        return RedirectToAction("Index");
    }

    public IActionResult Remove(int? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        var studioFromDatabase = _unitOfWork.StudioRepository.Get(x => x.Id == id);

        if (studioFromDatabase is null)
        {
            return NotFound();
        }

        return View(studioFromDatabase);
    }

    [HttpPost, ActionName("Remove")]
    [ValidateAntiForgeryToken]
    public IActionResult RemovePOST(int? id)
    {
        var studioFromDatabase = _unitOfWork.StudioRepository.Get(x => x.Id == id);

        if (studioFromDatabase is null)
        {
            return NotFound();
        }

        _unitOfWork.StudioRepository.Remove(studioFromDatabase);
        _unitOfWork.Save();

        TempData["Success"] = "The Studio has been removed successfully!";

        return RedirectToAction("Index");
    }
}