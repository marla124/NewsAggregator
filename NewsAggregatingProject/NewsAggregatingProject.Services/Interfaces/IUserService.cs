using NewsAggregatingProject.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregatingProject.Services.Interfaces
{
    public interface IUserService
    {
        public bool IsUserExists(string email);
        public Task<int> RegisterUser(UserDto userDto);
        public Task<bool> IsAdmin(string email);
        public Task<ClaimsIdentity> Authenticate(string email);
        public Task<bool> CheckPasswordCorrect(string email, string password);
        Task<UserDto> GetUserByEmail(string email);
        Task<UserDto> GetUserByRefreshToken(Guid requestRefreshToken);
    }
}
