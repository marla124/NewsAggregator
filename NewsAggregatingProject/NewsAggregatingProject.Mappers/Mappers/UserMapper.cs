using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data.Entities;
using NewsAggregatingProject.Models;
using Riok.Mapperly.Abstractions;
using System.ServiceModel.Syndication;

namespace NewsAggregatingProject.API.Mappers
{
    [Mapper]
    public partial class UserMapper
    {
        public partial UserDto RegisterUserRequestToUserDto(RegisterModel model);
        public partial UserDto LoginModelToUserDto(LoginModel model);
        public partial UserDto UserToUserDto(User user);





    }
}
