using MediatR;
using NewsAggregatingProject.API.Mappers;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data.CQS.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NewsAggregatingProject.Data.CQS.CommandsHandlers
{
    public class InsertRssDataCommandHandler : IRequestHandler<InsertRssDataCommand>
    {
        private readonly NewsAggregatingDBContext _dbContext;
        private readonly NewsMapper _newsMapper;
        public InsertRssDataCommandHandler(NewsAggregatingDBContext dbContext, NewsMapper newsMapper)
        {
            _dbContext = dbContext;
            _newsMapper = newsMapper;
        }

        public async Task Handle(InsertRssDataCommand command, CancellationToken cancellationToken)
        {
            var news = command.News
                .Select(dto=>_newsMapper.NewsDtoToNews(dto))
                .ToArray();
            await _dbContext.News.AddRangeAsync(news, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
