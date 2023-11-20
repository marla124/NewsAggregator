using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAggregating.Repositories
{
    public interface IUnitOfWork
    {
        IRepository Repository { get; }
        ISourceRepository SourceRepository { get; }
        Task<int> Commit();
    }
}
