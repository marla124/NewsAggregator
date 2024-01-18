using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Models;
using NewsAggregatingProject.Services.Interfaces;
using System.Security.Claims;

namespace NewsAggregatingProject.Controllers
{
    public class UserController : Controller
    {



        private readonly IUserService _userService;
        private readonly IValidator<UserRegisterModel> _registerValidator;
        private readonly IValidator<UserLoginModel> _loginValidator;


        public UserController(IValidator<UserRegisterModel> registerValidator, IUserService userService, IValidator<UserLoginModel> loginValidator)
        {
            _registerValidator = registerValidator;
            _userService = userService;
            _loginValidator = loginValidator;
        }


        public IActionResult Index() => View();


        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            var result = await _registerValidator.ValidateAsync(model);
            if (result.IsValid && !(_userService.IsUserExists(model.Email)))
            {
                var dto = new UserDto()
                {
                    Email = model.Email,
                    Password = model.Password
                };
                await _userService.RegisterUser(dto);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,  new ClaimsPrincipal(await _userService.Authenticate(dto.Email)));
                return Ok(model);
            }
            else
            {
                result.AddToModelState(ModelState);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var result = await _loginValidator.ValidateAsync(model);
            if (result.IsValid && _userService.IsUserExists(model.Email))
            {
                if (await _userService.IsPasswordCorrect(model.Email, model.Password))
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(await _userService.Authenticate(model.Email)));
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                result.AddToModelState(this.ModelState);
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult Login() => View();


        [HttpPost]
        public async Task<IActionResult> Logout(UserLogoutModel model)
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout() => View();

        [HttpPost]
        public IActionResult Test()
        {
            return Ok("Hello");
        }

    }
}
