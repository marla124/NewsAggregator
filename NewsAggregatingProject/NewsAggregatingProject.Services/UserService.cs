using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewsAggregatingProject.Core;
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


        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<ClaimsIdentity> Autanticate(string email)
        {
            var user = await _unitOfWork.UserRepository
                .FindBy(user => user.Email.Equals(email))
                .FirstOrDefaultAsync();
            if (user != null)
            {
                var status = (await _unitOfWork.UserStatusRepository.GetByIdAsNoTracking(user.UserStatusId)).Status;

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

            if (_unitOfWork.UserRepository.FindBy(user => user.Email.Equals(email)).Any())
            {
                return true;
            }
            return false;

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

        public async Task<bool> IsPasswordCorrect(string email, string password)
        {
            var currentPasswordHash = (await _unitOfWork.UserRepository
                .FindBy(user => user.Email.Equals(email))
                .FirstOrDefaultAsync())?
                .PasswordHash;

            var enteredPasswordHash = MdHashGenerate(password);

            return currentPasswordHash?.Equals(enteredPasswordHash) ?? false;
        }
    }
}
