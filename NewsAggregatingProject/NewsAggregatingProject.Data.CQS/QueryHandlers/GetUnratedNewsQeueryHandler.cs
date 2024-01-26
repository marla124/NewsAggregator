using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAggregatingProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregatingProject.Data.CQS.Queries
{
    internal class GetUnratedNewsQueryHandler : IRequestHandler<GetUnratedNewsQuery, Guid[]>
    {
        private readonly NewsAggregatingDBContext _dbContext;
        public GetUnratedNewsQueryHandler(NewsAggregatingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid[]> Handle(GetUnratedNewsQuery request, 
            CancellationToken cancellationToken)
        {
            return await _dbContext.News.AsNoTracking()
                .Where(news => news.Rate == null)
                .Take(request.MaxTake)
                .Select(news => news.Id)
                .ToArrayAsync(cancellationToken:cancellationToken);

        }
    }
}
