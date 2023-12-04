using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAggregatingProject.Models;
using NewsAggregatingProject.Repositories;

namespace NewsAggregatingProject.Controllers
{
    public class NewController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var newsList = await _unitOfWork.NewRepository
                .FindBy(news => !string.IsNullOrEmpty(news.Title),
                    news => news.Source)
                .Select(news => new NewModel()
                {
                    Id = news.Id,
                    DataAndTime = news.DataAndTime,
                    RatingScale = news.RatingScale.Status,
                    Title = news.Title,
                    Source = news.Source.Name,
                    Description = news.Description,
                })
                .ToListAsync();



            return View();
        }
    }
}

