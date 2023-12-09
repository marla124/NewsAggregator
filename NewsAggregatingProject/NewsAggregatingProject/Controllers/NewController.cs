using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsAggregatingProject.Data;
using NewsAggregatingProject.Data.Entities;
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
                .Select(news => new NewModel()
                {
                    Id = news.Id,
                    DataAndTime = news.DataAndTime,
                    Description = news.Description,
                    Title = news.Title

                }).ToList();
            return View(newsList);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateNewModel()
            {
                AvailableSources = await _unitOfWork.SourceRepository
                .GetAsQueryable()
                .AsNoTracking()
                .Select(source => new SelectListItem(source.Name, source.Id.ToString("D")))
                .ToListAsync()
            };
            return View(model); 
        }
    
    



    [HttpPost]
        public async Task<IActionResult> Create(CreateNewModel newModel)
        {
            if(ModelState.IsValid) 
            {
                var newForCreate = new News()
                {
                    Id = Guid.NewGuid(),
                    Title = newModel.Title,
                    Description = newModel.Description,
                    ContentNew = newModel.Description,
                    DataAndTime = DateTime.Now
                };
                await _unitOfWork.NewRepository.InsertOne(newForCreate);
                await _unitOfWork.Commit();
                return Ok();
            }
            else
            {
                return View(newModel);
            }

        }
    }
}



