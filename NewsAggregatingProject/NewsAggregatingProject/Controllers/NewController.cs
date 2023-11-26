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

        [HttpGet]
        public IActionResult Index()
        {
            var articlesList = _unitOfWork.NewRepository
                .FindBy(news => !string.IsNullOrEmpty(news.Title), news => news.Source)
                .Select(news => new NewsModel()
                {
                    Id = news.Id,
                    Title = news.Title,
                    Content = news.ContentNew
                }).ToListAsync();
                
                return View();

        }
    }
}
