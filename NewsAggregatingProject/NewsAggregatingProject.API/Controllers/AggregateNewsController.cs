using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsAggregatingProject.API.Mappers;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Models;
using NewsAggregatingProject.Services.Interfaces;

namespace NewsAggregatingProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AggregateNewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        public AggregateNewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        [HttpPost]
        public async Task<IActionResult> Aggregate(Guid sourceId)
        {
            //var data = await _newsService.AggregateDataFromByRssSourceId(sourceId);

            //var listFullfilledNews = new List<NewsDto>();
            //var existedNews = await _newsService.GetExistedNewsUrls();
            //var uniqueNews = data.Where(dto => !existedNews.Any(url => dto.SourceUrl.Equals(url))).ToArray();
            //foreach (var newsDto in uniqueNews)
            //{
            //    var fullFilledNews = await _newsService.GetNewsByUrl(newsDto.SourceUrl, newsDto);
            //    if (fullFilledNews != null)
            //    {
            //        listFullfilledNews.Add(fullFilledNews);
            //    }
            //}

            //await _newsService.ParseNewsText(listFullfilledNews);
            //await _newsService.RateBatchOfUnratedNews();
            RecurringJob.AddOrUpdate("News Rss Aggregate",
                () => _newsService.AggregateNewsFromRssBySourceId(sourceId),
                "0 0/3 * * *");
            RecurringJob.AddOrUpdate("Web page scrapping",
                () => _newsService.ParseNewsText(),
                "15 0/3 * * *");

            RecurringJob.AddOrUpdate("News Rating",
            () => _newsService.RateBatchOfUnratedNews(),
            "0 0/3 * * *");
            return Ok();
        }
        
    }

}
