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
    internal class GetUserByRefreshTokenQueryHandler : IRequestHandler<GetUserByRefreshTokenQuery, User>
    {
        private readonly NewsAggregatingDBContext _dbContext;
        public GetUserByRefreshTokenQueryHandler(NewsAggregatingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Handle(GetUserByRefreshTokenQuery request, 
            CancellationToken cancellationToken)
        {
            var refreshToken = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Id.Equals(request.RefreshTokenId),
                cancellationToken: cancellationToken);
            if (refreshToken == null) { return null; }
            else
            {
                var user = await _dbContext.Users
            .FirstOrDefaultAsync(us => us.Id.Equals(refreshToken.UserId),
                cancellationToken: cancellationToken);
                return user;
            }

        }
    }
}
