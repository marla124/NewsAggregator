using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NewsAggregatingProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentsById()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateComments()
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComments()
        {
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateComments()
        {
            return Ok();
        }

    }
}
