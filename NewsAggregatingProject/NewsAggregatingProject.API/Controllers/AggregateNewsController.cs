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
            var data = await _newsService.AggregateDataFromByRssSourceId(sourceId);

            var listFullfilledNews = new List<NewsDto>();
            var existedNews = await _newsService.GetExistedNewsUrls();
            var uniqueNews = data.Where(dto => !existedNews.Any(url => dto.SourceUrl.Equals(url))).ToArray();
            foreach (var newsDto in uniqueNews)
            {
                var fullFilledNews = await _newsService.GetNewsByUrl(newsDto.SourceUrl, newsDto);
                if (fullFilledNews != null)
                {
                    listFullfilledNews.Add(fullFilledNews);
                }
            }

            await _newsService.InsertParsedNews(listFullfilledNews);
            await _newsService.RateUnratedNews();
            return Ok();
        }
        
    }

}
