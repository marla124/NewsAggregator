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
            var articles = await _dbContext.News.Where(article => command.NewsData.Keys
                .Contains(article.Id))
                .ToListAsync(cancellationToken);

            foreach (var article in articles)
            {
                article.ContentNew = command.NewsData[article.Id];
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

        }

    }
}
