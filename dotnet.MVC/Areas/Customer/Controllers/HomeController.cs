using dotnet.DataAccess.Repository.IRepository;
using dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace dotnet.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Game> gameList = _unitOfWork.GameRepository.GetAll(includeProperties: "Studio,Genre");
        return View(gameList);
    }

    public IActionResult Details(int? id)
    {
        Game game = _unitOfWork.GameRepository.Get(x => x.Id == id, includeProperties: "Studio,Genre");

        if (game is null)
        {
            return NotFound();
        }

        return View(game);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}