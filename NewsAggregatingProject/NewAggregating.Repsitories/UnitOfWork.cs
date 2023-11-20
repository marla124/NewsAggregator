using NewAggregating.Repsitories;
using NewsAggregatingProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAggregating.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NewsAggregatingDBContext _dbContext;
        private readonly IRepository _repository;
        private readonly ISourceRepository _sourceRepository;

        public UnitOfWork(NewsAggregatingDBContext dbContext, IRepository repository, ISourceRepository sourceRepository)
        {
            _dbContext = dbContext;
            _repository = repository;
            _sourceRepository = sourceRepository;
        }

        public IRepository Repository => _repository;

        public ISourceRepository SourceRepository =>_sourceRepository;

        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
