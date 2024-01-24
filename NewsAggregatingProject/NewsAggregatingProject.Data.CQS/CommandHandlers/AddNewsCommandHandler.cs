using MediatR;
using NewsAggregatingProject.API.Mappers;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data.CQS.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NewsAggregatingProject.Data.CQS.CommandsHandlers
{
    public class AddNewsCommandHandler:IRequestHandler<AddNewsCommand>
    {
        private readonly NewsAggregatingDBContext _dbContext;
        private readonly NewsMapper _newsMapper;
        public AddNewsCommandHandler(NewsAggregatingDBContext dbContext, NewsMapper newsMapper)
        {
            _dbContext = dbContext;
            _newsMapper = newsMapper;
        }

        public async Task Handle(AddNewsCommand command, CancellationToken cancellationToken)
        {
            var news = _newsMapper.NewsDtoToNews(command.News);
            await _dbContext.News.AddAsync(news, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
