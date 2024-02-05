using Hangfire;
using Microsoft.AspNetCore.Mvc;
using NewsAggregatingProject.Services.Interfaces;

namespace NewsAggregatingProject.Controllers
{
    public class NewsAggregateController : Controller
    {
        private readonly INewsService _newsService;
        public NewsAggregateController(INewsService newsService)
        {
            _newsService = newsService;
        }
        [HttpPost]
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
