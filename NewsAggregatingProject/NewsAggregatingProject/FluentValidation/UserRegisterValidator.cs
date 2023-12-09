using FluentValidation;
using NewsAggregatingProject.Models;
using NewsAggregatingProject.Repositories;

namespace NewsAggregatingProject.FluentValidation
{
    public class UserRegisterValidator:AbstractValidator<UserRegisterModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserRegisterValidator(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;

            RuleFor(regUser => regUser.Email)
                .EmailAddress()
                .NotEmpty()
                .NotNull();
            //.NotEqual(model=>UserService.ChecEmail(model.Email));
            RuleFor(regUser => regUser.Password).NotNull()
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(64);
                //.Matches("");
            RuleFor(regUser => regUser.PasswordConfirmation)
                .Equal(model => model.Password);
        }
    }
}
