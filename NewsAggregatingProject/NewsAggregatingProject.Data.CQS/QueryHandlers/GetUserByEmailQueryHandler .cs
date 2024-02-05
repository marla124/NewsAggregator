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
    internal class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, User>
    {
        private readonly NewsAggregatingDBContext _dbContext;
        public GetUserByEmailQueryHandler(NewsAggregatingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Handle(GetUserByEmailQuery request, 
            CancellationToken cancellationToken)
        {
            var user= await _dbContext.Users.FirstOrDefaultAsync(us=>us.Email.Equals(request.Email), 
                cancellationToken: cancellationToken);
            return user;

        }
    }
}
