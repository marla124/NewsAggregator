using NewsAggregatingProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAggregating.Repositories
{
    public interface IRepository
    {
        Task<New?> GetById(Guid id);
        Task<List<New?>> Get();
        IQueryable<New?> GetAsQueryableAsync();
        Task InsertNews(IEnumerable<New?> news);
        Task InsertOneNew(New oneNew);
        Task DeleteById(Guid id);
        Task DeleteNews(IEnumerable<New?> news);
    }
}
