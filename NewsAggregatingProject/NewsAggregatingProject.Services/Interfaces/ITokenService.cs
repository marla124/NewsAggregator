using NewsAggregatingProject.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregatingProject.Services.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateJwtToken(UserDto userDto);
        public Task<Guid> AddRefreshToken(string requestEmail, string ipAddress);
        Task<bool> CheckRefreshToken(Guid requestRefreshToken);
        Task RemoveRefreshToken(Guid requestRefreshToken);
    }
}
