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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserMapper _userMapper;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, UserMapper userMapper, ITokenService tokenService)
        {
            _userService = userService;
            _userMapper = userMapper;
            _tokenService = tokenService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsersById()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterModel request)
        {
            var userDto=_userMapper.RegisterUserRequestToUserDto(request);
            await _userService.RegisterUser(userDto);

            var user = await _userService.GetUserByEmail(userDto.Email);
            //var token=await _tokenService.GenerateJwtToken(user); 
            return Created($"users/{user.Id}", null);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers()
        {
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUsers()
        {
            return Ok();
        }

    }
}
