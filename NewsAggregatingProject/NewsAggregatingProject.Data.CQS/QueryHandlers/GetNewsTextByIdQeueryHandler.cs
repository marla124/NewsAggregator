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
    internal class GetNewsTextByIdQeueryHandler : IRequestHandler<GetNewsTextByIdQeuery, string>
    {
        private readonly NewsAggregatingDBContext _dbContext;
        public GetNewsTextByIdQeueryHandler(NewsAggregatingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(GetNewsTextByIdQeuery request, 
            CancellationToken cancellationToken)
        {
            var news= await _dbContext.News.FirstOrDefaultAsync(news=>news.Equals(request.Id), 
                cancellationToken: cancellationToken);
            if (news == null)
            {
                return null;
            }
            return news?.ContentNew;

        }
    }
}
