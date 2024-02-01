using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data.Entities;
using System.Linq.Expressions;


namespace NewsAggregatingProject.Repositories
{
    public interface IRepository<T> where T : class, IBaseEntity
    {
        Task<T?> GetById(Guid id, params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsNoTracking(Guid id);

        IQueryable<T> FindBy(Expression<Func<T, bool>> wherePredicate,
                params Expression<Func<T, object>>[] includes);
        IQueryable<T?> GetAsQueryable();

        Task InsertMany(IEnumerable<T?> entities);
        Task InsertOne(T oneEntity);

        Task DeleteById(Guid id);
        Task DeleteMany(IEnumerable<T?> entities);
        Task<int> Count();
        Task Update(T entity);
        Task Patch(Guid id, List<PatchDto> patchDtos);

    }
}
