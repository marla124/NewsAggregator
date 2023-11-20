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
        Task<List<New>> GetNews();
        Task<IQueryable<New>> GetNewsWithSource();
        Task InsertNews(IEnumerable<New?> news);
        Task<New> GetBy(Guid id);
    }
}
