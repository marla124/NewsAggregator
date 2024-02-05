using Microsoft.AspNetCore.Mvc;
using NewsAggregatingProject.Models;
using NewsAggregatingProject.Services.Interfaces;

namespace NewsAggregatingProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;



        public TokenController(ITokenService tokenService,IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;

        }
        [HttpPost]
        [Route("Refresh")]

        public async Task<IActionResult> GenerateToken(RefreshTokenModel request)
        {
            var isRefreshTokenValid=await _tokenService.CheckRefreshToken(request.RefreshToken);
            if (isRefreshTokenValid)
            {
                var userDto = await _userService.GetUserByRefreshToken(request.RefreshToken);
                var jwtToken = await _tokenService.GenerateJwtToken(userDto);
                var refreshToken = await _tokenService.AddRefreshToken(userDto.Email,
                    HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                await _tokenService.RemoveRefreshToken(request.RefreshToken);
                return Ok(new TokenResponseModel { JwtToken = jwtToken, RefreshToken = refreshToken });
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateToken(LoginModel request)
        {
            var isUserCorrect = await _userService.CheckPasswordCorrect(request.Email, request.Password);
            if (isUserCorrect)
            {

                var userDto = await _userService.GetUserByEmail(request.Email);
                var jwtToken = await _tokenService.GenerateJwtToken(userDto);
                var refreshToken = await _tokenService.AddRefreshToken(userDto.Email,
                    HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                return Ok(new TokenResponseModel { JwtToken = jwtToken, RefreshToken = refreshToken });
            }

            return Unauthorized();
        }

        [HttpDelete]
        [Route("Revoke")]
        public async Task<IActionResult> RevokeToken(RefreshTokenModel request)
        {
            var IsRefreshTokenValue = await _tokenService.CheckRefreshToken(request.RefreshToken);
            //if (IsRefreshTokenValid)
            //{
                await _tokenService.RemoveRefreshToken(request.RefreshToken);
                return Ok();
            //}
            //return Unauthorized(); //!!!
        }

    }
}
