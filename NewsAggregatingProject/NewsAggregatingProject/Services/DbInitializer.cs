using NewsAggregatingProject.Data;
using NewAggregating.Repsitories;
using NewsAggregatingProject.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace NewsAggregatingProject.MVC7.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly SourceRepository _repository;

        public DbInitializer(SourceRepository repository)
        {
            _repository = repository;
        }


        public async Task InitDbWithTestValues()
        {
            //if (!_context.Sources.Any())
            //{
            //    var sourse = new Source()
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Name",
            //        Url = "yftyftyftyt"
            //    };
            //    await _context.Sources.AddAsync(sourse);
            //    await _context.SaveChangesAsync();
            //}
        }

        public async Task InsertArticles()
        {
            //var sourse = _context.Sources.AsNoTracking().FirstOrDefault();
            var news = new List<New>()
            {
                new New()
                {
                    Id=Guid.NewGuid(),
                    Title="TitleNew1",
                    DataAndTime=DateTime.Now,
                    ContentNew="Opisanie blblabla",
                    //Id_source=sourse.Id
                }
            };
            await _repository.InsertNews(news);
        }
    }
}
