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
    internal class GetNewsWithoutTextQueryHandler : IRequestHandler<GetNewsWithoutTextQuery, List<(Guid,string)>>
    {
        private readonly NewsAggregatingDBContext _dbContext;
        public GetNewsWithoutTextQueryHandler(NewsAggregatingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<(Guid, string)>> Handle(GetNewsWithoutTextQuery request,
            CancellationToken cancellationToken)
        {
            var news = await _dbContext.News
                .Where(news=>string.IsNullOrEmpty(news.ContentNew))
                .Select(news=> new {news.Id,news.SourceUrl})
                .ToListAsync(cancellationToken);
            var resultList = news.Select(arg => (arg.Id, arg.SourceUrl)).ToList();
            return resultList;
            

        }
    }
}
