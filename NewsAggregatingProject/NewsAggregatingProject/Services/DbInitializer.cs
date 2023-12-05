//using NewsAggregatingProject.Data;
//using NewsAggregatingProject.Data.Entities;

//namespace NewsAggregatingProject.Services
//{
//    public class DbInitializer : IDbInitializer
//    {
//        //private readonly Repository _repository;
//        private readonly NewsAggregatingDBContext _context;

//        public DbInitializer(/*Repository repository*/ NewsAggregatingDBContext context)
//        {
//            //_repository = repository;
//            _context = context;
//        }


//        public async Task InitDbWithTestValues()
//        {
//            var source = new Source()
//            {
//                Id = Guid.NewGuid(),
//                Name = "Name",
//                Url = "yftyftyftyt",
//                Description = "Description"
//            };
//            await _context.Sources.AddAsync(source);

//            var news = new List<News>()
//            {
//                new News()
//                {
//                    Id = Guid.NewGuid(),
//                    Title = "TitleNew1",
//                    DataAndTime = DateTime.Now,
//                    ContentNew = "Opisanie blblabla",
//                    Description = "Description",
//                    SourceId = source.Id // использование свойства Id созданного объекта source
//                },
//                new News()
//                {
//                    Id = Guid.NewGuid(),
//                    Title = "TitleNew1",
//                    DataAndTime = DateTime.Now,
//                    ContentNew = "Opisanie blblabla",
//                    Description = "Description",
//                    SourceId = source.Id // использование свойства Id созданного объекта source
//                }
//            };

//            await _context.News.AddRangeAsync(news);
//            await _context.SaveChangesAsync();

//        }
//    }
//}

