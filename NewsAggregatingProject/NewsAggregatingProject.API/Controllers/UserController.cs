using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsAggregatingProject.API.Mappers;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Models;
using NewsAggregatingProject.Services;
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


        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterModel request)
        {
            var userDto=_userMapper.RegisterUserRequestToUserDto(request);
            await _userService.RegisterUser(userDto);

            var user = await _userService.GetUserByEmail(userDto.Email);
            return Created($"users/{user.Id}", null);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(Guid id)
        {
            await _userService.DeleteUser(id);
            return Ok();
        }

 

    }
}
