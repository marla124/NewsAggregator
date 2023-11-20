using NewsAggregatingProject.Data;
using NewsAggregatingProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using NewAggregating.Repositories;

namespace NewAggregating.Repsitories
{
    public class SourceRepository: ISourceRepository
    {
        private readonly NewsAggregatingDBContext _dbContext;

        public SourceRepository(NewsAggregatingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Source>> GetAll()
        {
            return await _dbContext.Sources.ToListAsync();
        }

        public IQueryable<Source> GetAsQureable()
        {
            return _dbContext.Sources.AsQueryable();
        }

        public async Task<Source?> GetBy(Guid id)
        {
            return await _dbContext.Sources.FirstOrDefaultAsync(source => source.Equals(id));
        }

        public Task InsertNews(List<New> news)
        {
            throw new NotImplementedException();
        }
    }
}