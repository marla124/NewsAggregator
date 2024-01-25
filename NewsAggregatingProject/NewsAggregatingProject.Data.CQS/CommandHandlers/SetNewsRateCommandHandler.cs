using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAggregatingProject.API.Mappers;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data.CQS.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NewsAggregatingProject.Data.CQS.CommandsHandlers
{
    public class SetNewsRateCommandHandler : IRequestHandler<SetNewsRateCommand>
    {
        private readonly NewsAggregatingDBContext _dbContext;
        public SetNewsRateCommandHandler(NewsAggregatingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(SetNewsRateCommand request, CancellationToken cancellationToken)
        {
            var news = await _dbContext.News.FirstOrDefaultAsync(news => news.Id.Equals(request.Id), cancellationToken);
            news.Rate = request.Rate;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
