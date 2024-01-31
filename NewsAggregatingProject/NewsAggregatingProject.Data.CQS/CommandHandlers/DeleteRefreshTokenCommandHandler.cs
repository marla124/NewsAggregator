using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAggregatingProject.Data;
using WebApp.Data.CQS.Commands;

namespace WebApp.Data.CQS.CommandHandlers
{

    public class DeleteRefreshTokenCommandHandler : IRequestHandler<DeleteRefreshTokenCommand>
    {
        private readonly NewsAggregatingDBContext _dbContext;


        public DeleteRefreshTokenCommandHandler(NewsAggregatingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var rt = await _dbContext.RefreshTokens.FirstOrDefaultAsync(token =>
                token.Id.Equals(command.Id), cancellationToken);
            _dbContext.RefreshTokens.Remove(rt);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}