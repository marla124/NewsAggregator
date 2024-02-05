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
    internal class GetRefreshtokenQueryHandler : IRequestHandler<GetRefreshtokenQuery, RefreshToken>
    {
        private readonly NewsAggregatingDBContext _dbContext;
        public GetRefreshtokenQueryHandler(NewsAggregatingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RefreshToken> Handle(GetRefreshtokenQuery request, 
            CancellationToken cancellationToken)
        {
            var refToken= await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt=>rt.Id.Equals(request.Id), 
                cancellationToken: cancellationToken);
            return refToken;

        }
    }
}
