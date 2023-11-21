using Microsoft.EntityFrameworkCore;
using NewAggregating.Repositories;
using NewsAggregatingProject.Data;
using NewsAggregatingProject.Data.Entities;

namespace NewAggregating.Repsitories
{
    public class Repository: IRepository
    {
        private readonly NewsAggregatingDBContext _dbContext;

        public Repository(NewsAggregatingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteById(Guid id)
        {
            var deleteEntity = await GetById(id);
            if (deleteEntity != null)
            {
                _dbContext.News.Remove(deleteEntity);
            }
        }

        public async Task DeleteNews(IEnumerable<New?> news)
        {
            if (news.Any())
            {
                var deleteEntities = news.Where(news => _dbContext.News.Any(dbNew => dbNew.Id.Equals(news.Id))).ToList();
                _dbContext.News.RemoveRange(deleteEntities);
            }   
        }

        public async Task<List<New?>> Get()
        {
            return await _dbContext.News.ToListAsync();
        }

        public IQueryable<New?> GetAsQueryableAsync()
        {
            return _dbContext.News.AsQueryable();
        }

        public async Task<New?> GetById(Guid id)
        {
            return await _dbContext.News.FirstOrDefaultAsync(news=>news.Id.Equals(id));
        }

        public async Task InsertNews(IEnumerable<New?> news)
        {
            await _dbContext.News.AddRangeAsync(news);
        }

        public async Task InsertOneNew(New oneNew)
        {
            await _dbContext.News.AddAsync(oneNew); 
        }
    }
}

