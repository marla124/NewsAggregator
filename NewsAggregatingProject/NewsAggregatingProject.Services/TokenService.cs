using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data.CQS.Commands;
using NewsAggregatingProject.Data.CQS.Queries;
using NewsAggregatingProject.Data.Entities;
using NewsAggregatingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApp.Data.CQS.Commands;

namespace NewsAggregatingProject.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public TokenService(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        public async Task<Guid> AddRefreshToken(string requestEmail, string ipAddress)
        {
            var user = await _mediator.Send(new GetUserByEmailQuery() { Email = requestEmail });
            var refreshToken = await _mediator.Send(new AddRefreshTokenCommand() { UserId = user.Id, Ip=ipAddress });
            return refreshToken;
        }

        public async Task<bool> CheckRefreshToken(Guid requestRefreshToken)
        {
            var refToken=await _mediator.Send(new GetRefreshtokenQuery() { Id=requestRefreshToken });
            if(refToken != null&&refToken.ExpiringAt.ToUniversalTime()<DateTime.UtcNow) {
                return true;
            }
            return false;   
        }

        public async Task<string> GenerateJwtToken(UserDto userDto)
        {
            var isLifetime = int.TryParse(_configuration["Jwt:Lifetime"], out var lifetime);
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);
            var iss = _configuration["Jwt:Issuer"];
            var aud = _configuration["Jwt:Audience"];



            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Email, userDto.Email),
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim("aud",aud),
                        new Claim("iss",iss)

                }),
                Expires = DateTime.UtcNow.AddMinutes(lifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task RemoveRefreshToken(Guid requestRefreshToken)
        {
            await _mediator.Send(new DeleteRefreshTokenCommand()
            {
                Id = requestRefreshToken
            });
        }
    }
}
