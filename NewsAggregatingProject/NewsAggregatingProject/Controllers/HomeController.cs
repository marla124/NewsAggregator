using Microsoft.AspNetCore.Mvc;
using NewsAggregatingProject.MVC7.Models;
using NewsAggregatingProject.MVC7.Services;
using System.Diagnostics;

namespace NewsAggregatingProject.MVC7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IDbInitializer _dbInitializer;

        public HomeController(ILogger<HomeController> logger /*IDbInitializer dbInitializer*/)
        {
            _logger = logger;
            //_dbInitializer = dbInitializer;
        }

        public async Task<IActionResult> Index()
        {
            //await _dbInitializer.InitDbWithTestValues();
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
    }
}