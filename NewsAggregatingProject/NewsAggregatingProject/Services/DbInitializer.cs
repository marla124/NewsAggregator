using NewsAggregatingProject.Data.Entities;
using NewsAggregatingProject.Data;

namespace NewsAggregatingProject.Services
{
    public class DbInitializer : IDbInitializer
    {
        //private readonly Repository _repository;
        private readonly NewsAggregatingDBContext _context;

        public DbInitializer(/*Repository repository*/ NewsAggregatingDBContext context)
        {
            //_repository = repository;
            _context = context;
        }


        public async Task InitDbWithTestValues()
        {
            if (!_context.Sources.Any())
            {
                var sourse = new Source()
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    Url = "yftyftyftyt",
                    Description = "Description"
                };
                await _context.Sources.AddAsync(sourse);
                InsertArticles();
                await _context.SaveChangesAsync();
            }
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
                    Description="Description",
                    //IdSource=Guid.NewGuid(),
                    //IdCategory=Guid.NewGuid(),
                    IdRating=Guid.NewGuid()
                }
            };
            foreach (var s in news)
            {
                await _context.News.AddAsync(s);
            }
            //await _repository.InsertNews(news);
        }
    }
}
