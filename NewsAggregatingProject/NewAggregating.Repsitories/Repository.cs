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

        public async Task<List<New?>> GetArticles()
        {
            return await _dbContext.News.ToListAsync();
        }

        public IQueryable<New?> GetNewsWithSource()
        {
            return _dbContext.News.Include(news => news.Source);
        }

        public async Task InsertArticles(IEnumerable<New?> news)
        {
            await _dbContext.News.AddRangeAsync(news);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<New?> GetById(Guid id)
        {
            return await _dbContext.News.FirstOrDefaultAsync(news => news.Id.Equals(id));
        }

        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}

