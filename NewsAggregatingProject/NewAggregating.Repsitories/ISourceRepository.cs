using NewsAggregatingProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAggregating.Repositories
{
    public interface ISourceRepository
    {
        Task<Source?> GetById(Guid id);
        Task<List<Source?>> Get();
        IQueryable<Source?> GetAsQueryableAsync();
        Task InsertSources(IEnumerable<Source?> sources);
        Task InsertOneSource(Source oneSource);
        Task DeleteById(Guid id);
        Task DeleteSources(IEnumerable<Source?> sources);
    }
}
