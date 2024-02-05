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
    internal class GetNewsTextByIdQueryHandler : IRequestHandler<GetNewsTextByIdQuery, string>
    {
        private readonly NewsAggregatingDBContext _dbContext;
        public GetNewsTextByIdQueryHandler(NewsAggregatingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(GetNewsTextByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var news= await _dbContext.News.FirstOrDefaultAsync(news=>news.Id.Equals(request.Id), 
                cancellationToken: cancellationToken);
            if (news == null)
            {
                return null;
            }
            return news?.ContentNew;

        }
    }
}
