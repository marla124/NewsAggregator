using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewsAggregatingProject.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewsAggregatingProject.API.Controllers
{
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult Login(LoginModel request)
        {
            if (request.Email.Equals("admin@email.com") && request.Password.Equals("Password"))
            {
                var isLifetime= int.TryParse(_configuration["Jwt:Lifetime"], out var lifetime);
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);
                var iss = _configuration["Jwt:Issuer"];
                var aud = _configuration["Jwt:Audience"];



                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, request.Email),
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim("aud",aud),
                        new Claim("iss",iss)

                    }),
                    Expires = DateTime.UtcNow.AddMinutes(lifetime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };

                var tokenHandler=new JwtSecurityTokenHandler();
                var token=tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new {Token=tokenHandler.WriteToken(token) });
            }
            return Unauthorized();
        }
    }
}
