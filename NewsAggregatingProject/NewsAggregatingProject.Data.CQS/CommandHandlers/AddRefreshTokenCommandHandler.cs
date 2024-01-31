using MediatR;
using NewsAggregatingProject.API.Mappers;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data.CQS.Commands;
using NewsAggregatingProject.Data.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NewsAggregatingProject.Data.CQS.CommandsHandlers
{
    public class AddRefreshTokenCommandHandler : IRequestHandler<AddRefreshTokenCommand, Guid>
    {
        private readonly NewsAggregatingDBContext _dbContext;
        public AddRefreshTokenCommandHandler(NewsAggregatingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(AddRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var refToken = new RefreshToken()
                {
                    Id = Guid.NewGuid(),
                    GeneratedAt = DateTime.UtcNow,
                    ExpiringAt = DateTime.UtcNow.AddHours(1),
                    AssociateDeviceName = command.Ip,
                    UserId = command.UserId,

                };
                await _dbContext.RefreshTokens.AddAsync(refToken, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return refToken.Id;
            }
            catch (Exception ex)
            {
                //log
                return Guid.Empty;
            }
            
        }
    }
}
