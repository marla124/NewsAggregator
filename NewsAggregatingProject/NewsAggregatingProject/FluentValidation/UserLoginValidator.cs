using FluentValidation;
using NewsAggregatingProject.Models;
using NewsAggregatingProject.Repositories;

namespace NewsAggregatingProject.FluentValidation
{
    public class UserLoginValidator:AbstractValidator<UserLoginModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserLoginValidator(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;

            RuleFor(logUser => logUser.Email)
                .EmailAddress()
                .NotEmpty()
                .NotNull();
            RuleFor(logUser => logUser.Password).NotNull()
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(64);
        }
    }
}
