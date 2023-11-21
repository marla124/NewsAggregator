using Microsoft.EntityFrameworkCore;
using NewAggregating.Repositories;
using NewsAggregatingProject.Data;
using NewsAggregatingProject.Data.Entities;
using System.Data;

namespace NewAggregating.Repsitories
{
    public class Repository<T>: IRepository<T> where T : class, IBaseEntity
    {
        private readonly NewsAggregatingDBContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(NewsAggregatingDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet= _dbContext.Set<T>();
        }

        public async Task DeleteById(Guid id)
        {
            var deleteEntity = await GetById(id);
            if (deleteEntity != null)
            {
                _dbSet.Remove(deleteEntity);
            }
            else
            {
                throw new ArgumentException("Incorrect id for delete", nameof(id));
            }
        }

        public async Task DeleteMany(IEnumerable<T> entities)
        {
            if (entities.Any())
            {
                var deleteEntities = entities.Where(entity => _dbSet.Any(dbEn => dbEn.Id.Equals(entity.Id))).ToList();
                _dbSet.RemoveRange(deleteEntities);
            }   
        }

        public async Task<List<T>> Get()
        {
            return await _dbSet.ToListAsync();
        }

        public IQueryable<T> GetAsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(entities=>entities.Id.Equals(id));
        }

        public async Task InsertMany(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task InsertOne(T entity)
        {
            await _dbSet.AddAsync(entity); 
        }
    }
}

