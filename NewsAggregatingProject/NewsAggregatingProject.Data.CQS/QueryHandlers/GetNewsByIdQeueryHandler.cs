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
    internal class GetNewsByIdQeueryHandler : IRequestHandler<GetNewsByIdQeuery, News>
    {
        private readonly NewsAggregatingDBContext _dbContext;
        public GetNewsByIdQeueryHandler(NewsAggregatingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<News> Handle(GetNewsByIdQeuery request, 
            CancellationToken cancellationToken)
        {
            var news= await _dbContext.News.FirstOrDefaultAsync(news=>news.Equals(request.Id), 
                cancellationToken: cancellationToken);
            return news;

        }
    }
}
