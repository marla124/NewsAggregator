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
        Task<List<New?>> GetArticles();
        IQueryable<New?> GetNewsWithSource();
        Task InsertArticles(IEnumerable<New?> articles);
        Task<New?> GetById(Guid id);
        Task<int> Commit();
    }
}
