using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAggregatingProject.API.Mappers;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data.CQS.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NewsAggregatingProject.Data.CQS.CommandsHandlers
{
    public class UpdateNewsTextHandler : IRequestHandler<UpdateNewsText>
    {
        private readonly NewsAggregatingDBContext _dbContext;
        private readonly NewsMapper _newsMapper;
        public UpdateNewsTextHandler(NewsAggregatingDBContext dbContext, NewsMapper newsMapper)
        {
            _dbContext = dbContext;
            _newsMapper = newsMapper;
        }

        public async Task Handle(UpdateNewsText command, CancellationToken cancellationToken)
        {
            var news = await _dbContext.News
                .Where(newone => command.NewsData.Keys
                .Contains(newone.Id))
                .ToListAsync(cancellationToken);

            foreach (var newone in news)
            {
                newone.ContentNew = command.NewsData[newone.Id];
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

        }

    }
}
