using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewAggregating.Repositories;
using NewsAggregatingProject.MVC7.Models;

namespace NewsAggregatingProject.MVC7.Controllers
{
    public class NewController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var newsList = await _unitOfWork.NewRepository
                .FindBy(article => !string.IsNullOrEmpty(article.Title),
                    news => news.Source)
                .Select(news => new NewsModel()
                {
                    Id = news.Id,
                    DataAndTime = news.DataAndTime,
                    RatingScale = news.RatingScale.Status,
                    Title = news.Title,
                    Source = news.Source.Name,
                    Description = news.Description,
                })
                .ToListAsync();

            

            return View(newsList);
        }
    }
}

