using Microsoft.AspNetCore.Mvc;
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
            //var articlesList = _unitOfWork.Repository.GetNewsWithSource()
            //    .Select(news => new NewsModel()
            //    {
            //        Id = news.Id,
            //        Title = news.Title
            //    };
                return View();

        }
    }
}
