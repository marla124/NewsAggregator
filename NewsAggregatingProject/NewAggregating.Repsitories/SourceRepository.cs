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

        public async Task DeleteById(Guid id)
        {
            var deleteEntity = await GetById(id);
            if (deleteEntity != null)
            {
                _dbContext.Sources.Remove(deleteEntity);
            }
        }

        public async Task DeleteSources(IEnumerable<Source?> sources)
        {
            if (sources.Any())
            {
                var deleteEntities = sources.Where(sources => _dbContext.Sources.Any(dbSource => dbSource.Id.Equals(sources.Id))).ToList();
                _dbContext.Sources.RemoveRange(deleteEntities);
            }
        }

        public async Task<List<Source?>> Get()
        {
            return await _dbContext.Sources.ToListAsync();
        }

        public IQueryable<Source?> GetAsQueryableAsync()
        {
            return _dbContext.Sources.AsQueryable();
        }

        public async Task<Source?> GetById(Guid id)
        {
            return await _dbContext.Sources.FirstOrDefaultAsync(sources => sources.Id.Equals(id));
        }

        public async Task InsertSources(IEnumerable<Source?> sources)
        {
            await _dbContext.Sources.AddRangeAsync(sources);
        }

        public async Task InsertOneSource(Source oneSource)
        {
            await _dbContext.Sources.AddAsync(oneSource);
        }
    }
}