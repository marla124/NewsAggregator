using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data;
using NewsAggregatingProject.Data.Entities;
using NewsAggregatingProject.Filters;
using NewsAggregatingProject.Models;
using NewsAggregatingProject.Repositories;
using NewsAggregatingProject.Services;
using NewsAggregatingProject.Services.Interfaces;
using System.Data;


namespace NewsAggregatingProject.Controllers
{
    [LastVisitTrackerResourceFilter]
    public class NewsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly NewsAggregatingDBContext _dbContext;
        private readonly INewsService _newsService;
        public NewsController(IUnitOfWork unitOfWork, NewsAggregatingDBContext dBContext, INewsService newsService)
        {
            _newsService = newsService;
            _unitOfWork = unitOfWork;
            _dbContext = dBContext;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> Index()
        {
            try
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
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetNews(Guid sourceId)
        {
            var source= await _unitOfWork.SourceRepository.GetById(sourceId);
            var model = new SourceModelAggregating()
            {
                Id = sourceId,
                Title = source.Name
            };
            return View(model);
        }
        //[HttpPost]
        //public async Task<IActionResult> Aggregate(SourceModelAggregating model)
        //{
        //    var data= await _newsService.AggregateDataFromByRssSourceId(model.Id);

        //    var listFullfilledNews = new List<NewsDto>();
        //    var existedNews = await _newsService.GetExistedNewsUrls();
        //    var uniqueNews = data.Where(dto => !existedNews.Any(url => dto.SourceUrl.Equals(url))).ToArray();
        //    foreach (var newsDto in uniqueNews)
        //    {
        //        var fullFilledNews= await _newsService.GetNewsByUrl(newsDto.SourceUrl, newsDto);
        //        if (fullFilledNews != null)
        //        {
        //            listFullfilledNews.Add(fullFilledNews);
        //        }
        //    }


        //    await _newsService.ParseNewsText(listFullfilledNews);
        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateNewModel()
            {
                AvailableSources = await _unitOfWork.SourceRepository
                .GetAsQueryable().AsNoTracking()
                .Select(source => new SelectListItem(source.Name, source.Id.ToString("D"))).ToListAsync()
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
                    DataAndTime = DateTime.Now,
                   
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



