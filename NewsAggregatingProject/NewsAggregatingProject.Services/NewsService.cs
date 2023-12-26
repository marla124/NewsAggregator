using NewsAggregatingProject.Core;
using NewsAggregatingProject.Repositories;
using NewsAggregatingProject.Services.Interfaces;
using System.ServiceModel.Syndication;
using System.Xml;

namespace NewsAggregatingProject.Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<NewsDto[]?> AggregateDataFromByRssSourceId(Guid sourceId)
        {
            var sourceRss = (await _unitOfWork.SourceRepository.GetById(sourceId))?.RSSUrl;
            if (string.IsNullOrEmpty(sourceRss)) return null;
            using (var reader = XmlReader.Create(sourceRss))
            {
                var feed = SyndicationFeed.Load(reader);
                var rssNews = feed.Items.Select(item => new NewsDto()
                {

                    Id = Guid.NewGuid(),
                    SourceId = sourceId,
                    Date = item.PublishDate.UtcDateTime,
                    Title = item.Title.Text,
                    Description = item.Summary.Text,
                    SourceUrl=item.Id
                }).ToArray();

                return rssNews;
            }

        }


    }
}
