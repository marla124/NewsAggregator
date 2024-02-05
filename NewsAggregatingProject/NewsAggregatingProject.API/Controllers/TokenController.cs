using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewsAggregatingProject.API.Mappers;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Models;
using NewsAggregatingProject.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewsAggregatingProject.API.Controllers
{
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
            var IsRefreshTokenValue=await _tokenService.CheckRefreshToken(request.RefreshToken);
            if (IsRefreshTokenValue)
            {
                var userDto= await _userService.GetUserByRefreshToken(request.RefreshToken);

                var jwtToken = await _tokenService.GenerateJwtToken(userDto);
                var refreshToken = await _tokenService.AddRefreshToken(userDto.Email,
                    HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString());
                await _tokenService.RemoveRefreshToken(request.RefreshToken);
                return Ok(new TokenResponseModel { JwtToken = jwtToken, RefreshToken = refreshToken });
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateToken(LoginModel request)
        {
            //can be refactored
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
            //if (IsRefreshTokenValue)
            //{
                await _tokenService.RemoveRefreshToken(request.RefreshToken);
                return Ok();
            //}
            //return Unauthorized(); //!!!
        }

    }
}
