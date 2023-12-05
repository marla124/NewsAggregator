using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAggregatingProject.Data;
using NewsAggregatingProject.Models;
using NewsAggregatingProject.Repositories;

namespace NewsAggregatingProject.Controllers
{
    public class NewController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly NewsAggregatingDBContext _dbContext;

        public NewController(IUnitOfWork unitOfWork, NewsAggregatingDBContext dBContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var newsList = _dbContext.News
                .Include(news => news.Source)
                .Select(news=> new NewModel()
                    {
                        Id = news.Id,
                        DataAndTime = news.DataAndTime,
                        Description = news.Description,
                        Title = news.Title,
                        Source=news.Source.Name
                
                    }).ToList();


            return View(newsList);
        }
    }
}

