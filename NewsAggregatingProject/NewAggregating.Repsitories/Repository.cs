using Microsoft.EntityFrameworkCore;
using NewAggregating.Repositories;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data;
using NewsAggregatingProject.Data.Entities;
using System.Data;
using System.Linq.Expressions;

namespace NewAggregating.Repsitories
{
    public class Repository<T>: IRepository<T> where T : class, IBaseEntity
    {
        private readonly NewsAggregatingDBContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(NewsAggregatingDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet= _dbContext.Set<T>();
        }

        public virtual async Task DeleteById(Guid id)
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

        public virtual async Task DeleteMany(IEnumerable<T> entities)
        {
            if (entities.Any())
            {
                var deleteEntities = entities.Where(entity => _dbSet.Any(dbEn => dbEn.Id.Equals(entity.Id))).ToList();
                _dbSet.RemoveRange(deleteEntities);
            }   
        }

        public virtual async Task<List<T>> FindBy()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual IQueryable<T> GetAsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(entities=>entities.Id.Equals(id));
        }

        public virtual async Task InsertMany(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual async Task InsertOne(T entity)
        {
            await _dbSet.AddAsync(entity); 
        }

        public async Task<T?> GetByIdAsNoTracking(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(entities => entities.Id.Equals(id));
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> wherePredicate,
            params Expression<Func<T, object>>[] includes)
        {
            var resultQuery = _dbSet.Where(wherePredicate);
            if (includes.Any())
            {
                resultQuery = includes.Aggregate(resultQuery,
                    (current, include)
                        => current.Include(include));
            }
            return resultQuery;
        }

        public async Task<int> Count()
        {
            return await _dbSet.CountAsync();
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task Patch(Guid id, List<PatchDto> patchDtos)
        {
            var entity =await GetById(id);
            if(entity != null)
            {
                var nameValuePairProperties = patchDtos.ToDictionary(
                        k => k.PropertyName,
                        v => v.PropertyName);
                var dbEntityEntry=_dbContext.Entry(entity);
                dbEntityEntry.CurrentValues.SetValues(nameValuePairProperties);
                dbEntityEntry.State = EntityState.Modified;
            }
            else
            {
                throw new ArgumentException("Incorrect id for delete", nameof(id));
            }
        }
    }
}

