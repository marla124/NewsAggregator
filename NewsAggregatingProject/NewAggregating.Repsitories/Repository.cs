using NewsAggregatingProject.Data;
using NewsAggregatingProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using NewAggregating.Repositories;

namespace NewAggregating.Repsitories
{
    public class SourceRepository: ISourceRepository
    {
        private readonly NewsAggregatingDBContext _dbContext;
        private int info=10;

        public SourceRepository(NewsAggregatingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<New?>> GetNews()
        {
            return await _dbContext.News.ToListAsync();
        }

        public async Task<List<New>> GetNews()
        {
            info++;
            return await _dbContext.News.ToListAsync();
        }
        public async Task InsertNews(IEnumerable<New> news)
        {
            info++;
            await _dbContext.News.AddRangeAsync(news);
            await _dbContext.SaveChangesAsync();
        }
    }
}