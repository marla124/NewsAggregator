using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NewsAggregatingProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourceController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSuurceById()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetSourcess()
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSources()
        {
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateSources() => Ok();

    }
}
