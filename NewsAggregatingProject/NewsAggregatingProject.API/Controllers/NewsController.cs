using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsAggregatingProject.API.Mappers;
using NewsAggregatingProject.Services.Interfaces;

namespace NewsAggregatingProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly NewsMapper _newsMapper;
        public NewsController(INewsService newsService, NewsMapper newsMapper)
        {
            _newsService = newsService;
            _newsMapper = newsMapper;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var news = _newsMapper.NewsDtoToNewsModel( await _newsService.GetNewsById(id));
            return Ok(news);
        }


        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            var news = (await _newsService.GetPositive())
                .Select(dto=>_newsMapper.NewsDtoToNewsModel(dto))
                .ToArray();
            return Ok(news);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNews()
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNews(Guid id)
        {
            await _newsService.DeleteNews(id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateNews()
        {
            return Ok();
        }



    }

}
