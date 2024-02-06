using Hangfire;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Aggregate(Guid sourceId)
        {

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
