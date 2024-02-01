using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAggregatingProject.Filters;
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
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _logger.LogInformation("Hello from HomeController. Its works");
        }

        [TestResourceFilter(50,"HelloWorld!")]
        public async Task<IActionResult> Index(int? data)
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Privacy()
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
            var newsList = _unitOfWork.NewsRepository
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
