using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewsAggregatingProject.API.Mappers;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data.CQS.Queries;
using NewsAggregatingProject.Data.Entities;
using NewsAggregatingProject.Repositories;
using NewsAggregatingProject.Services.Interfaces;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NewsAggregatingProject.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly UserMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mediator = mediator;
        }

        public async Task<ClaimsIdentity> Authenticate(string email)
        {
            var user = await _unitOfWork.UserRepository
                .FindBy(us => us.Email.Equals(email))
                .FirstOrDefaultAsync();
            if (user != null)
            {
                var status = (await _unitOfWork.UserStatusRepository
                    .GetByIdAsNoTracking(user.UserStatusId)).Status;

                var claims = new List<Claim>()
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, status)
                };
                var claimsIdentity = new ClaimsIdentity(claims,
                    "ApplicationCookie",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }

        public async Task <bool> IsAdmin(string email)
        {
            ///
            var user = await _unitOfWork.UserRepository
                .FindBy(user => user.Email.Equals(email))
                .FirstOrDefaultAsync();
            return false;
        }
        public async Task<int> RegisterUser(UserDto userDto)
        {
            var userStatus = await _unitOfWork.UserStatusRepository
                .FindBy(status => status.Status
                .Equals("User")).FirstOrDefaultAsync();
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = userDto.Email,
                PasswordHash = MdHashGenerate(userDto.Password),
                UserStatusId = userStatus.Id
            };
            await _unitOfWork.UserRepository.InsertOne(user);

            return await _unitOfWork.Commit();
        }
        public bool IsUserExists(string email)
        {

            return _unitOfWork.UserRepository
                .FindBy(user => user.Email.Equals(email)).Any();


        }
        private string MdHashGenerate(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                var salt = _configuration["AppSettings:PasswordSalt"];
                byte[] inputBytes = Encoding.UTF8.GetBytes($"{input}{salt}");
                byte[] HashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(HashBytes);
            }
        }

        public async Task<bool> CheckPasswordCorrect(string email, string password)
        {
            var currentPasswordHash = (await _unitOfWork.UserRepository
                .FindBy(user => user.Email.Equals(email))
                .FirstOrDefaultAsync())?
                .PasswordHash;

            var enteredPasswordHash = MdHashGenerate(password);

            return currentPasswordHash?.Equals(enteredPasswordHash) ?? false;
        }

        public async Task<UserDto> GetUserByEmail(string userDtoEmail)
        {

            var query = new GetUserByEmailQuery() { Email = userDtoEmail };
            var user = await _mediator.Send(query);
            return _mapper.UserToUserDto(user);
        }

        public async Task<UserDto> GetUserByRefreshToken(Guid requestRefreshToken)
        {
            var user=await _mediator.Send(new GetUserByRefreshTokenQuery { RefreshTokenId = requestRefreshToken });
            var dto = _mapper.UserToUserDto(user);
            return dto;
        }

        public async Task DeleteUser(Guid id)
        {
            await _unitOfWork.UserRepository.DeleteById(id);
            await _unitOfWork.Commit();
        }
    }
}
