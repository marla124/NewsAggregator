using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAggregating.Repositories
{
    public interface IRepository<T> where T : class, IBaseEntity
    {
        Task<T?> GetById(Guid id);
        Task<T?> GetByIdAsNoTracking(Guid id);

        //Task<IQueryable<T?>> FindBy();
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
