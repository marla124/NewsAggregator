using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAggregatingProject.Models;
using NewsAggregatingProject.Repositories;
using NewsAggregatingProject.Services;
using System.Diagnostics;

namespace NewsAggregatingProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IDbInitializer _dbInitializer;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IDbInitializer dbInitializer)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _dbInitializer = dbInitializer;
        }

        public async Task<IActionResult> Index()
        {
            await _dbInitializer.InitDbWithTestValues();
            return View();
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

        [HttpGet]
        public async Task<IActionResult> Favorite()
        {
            var newsList = _unitOfWork.NewRepository
                .FindBy(news => !string.IsNullOrEmpty(news.Title), news => news.Source)
                .Select(news => new NewModel()
                {
                    Id = news.Id,
                    Title = news.Title,
                    Content = news.ContentNew
                }).ToListAsync();
            return View(newsList);
        }
    }
}
