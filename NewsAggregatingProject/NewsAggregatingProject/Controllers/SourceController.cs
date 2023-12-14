using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAggregatingProject.Models;
using NewsAggregatingProject.Repositories;

namespace NewsAggregatingProject.Controllers
{
    public class SourceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SourceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> List()
        {
            var sources = await _unitOfWork.SourceRepository
                .GetAsQueryable().ToListAsync();
            var model = sources.Select(source => new SourceModel()
            {
                Id = source.Id,
                Name = source.Name,
                RSSUrl = source.RSSUrl,
                Url = source.Url
            }).ToList();
            return View(model);
        }
    }
}
