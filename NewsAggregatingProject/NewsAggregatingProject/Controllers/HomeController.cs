﻿using Microsoft.AspNetCore.Mvc;
using NewsAggregatingProject.Models;
using NewsAggregatingProject.Services;
using System.Diagnostics;

namespace NewsAggregatingProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDbInitializer _dbInitializer;

        public HomeController(ILogger<HomeController> logger, IDbInitializer dbInitializer)
        {
            _logger = logger;
            _dbInitializer= dbInitializer;
        }

        public IActionResult Index()
        {
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