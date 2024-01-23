using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsAggregatingProject.API.Mappers;
using NewsAggregatingProject.Services;
using NewsAggregatingProject.Services.Interfaces;

namespace NewsAggregatingProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourceController : ControllerBase
    {
        private readonly ISourceService _sourceService;
        private readonly SourceMapper _sourceMapper;
        public SourceController(ISourceService sourceService, SourceMapper sourceMapper)
        {
            _sourceService = sourceService;
            _sourceMapper = sourceMapper;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSourceById(Guid id)
        {
            var source = _sourceMapper.SourceDtoToSourceModel(await _sourceService.GetSourceById(id));
            return Ok(source);
        }

        [HttpGet]
        public async Task<IActionResult> GetSources()
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSource(Guid id)
        {
            await _sourceService.DeleteSource(id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateSource() => Ok();

    }
}
