using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NewsAggregatingProject.Models;

namespace NewsAggregatingProject.Controllers
{
    public class UserController : Controller
    {




        private readonly IValidator<UserRegisterModel> _registerValidator;

        public UserController(IValidator<UserRegisterModel> registerValidator)
        {
            _registerValidator = registerValidator;
        }
            
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new UserRegisterModel();
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Register(UserRegisterModel model)
        {
            var result= await _registerValidator.ValidateAsync(model);
            if (result.IsValid)
            {
                return Ok(model);
            }
            else
            {
                result.AddToModelState(this.ModelState);
                return View(model);
            }
        }
    }
}
