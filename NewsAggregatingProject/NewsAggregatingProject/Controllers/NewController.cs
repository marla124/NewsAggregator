using Microsoft.AspNetCore.Mvc;

namespace NewsAggregatingProject.MVC7.Controllers
{
    public class NewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
