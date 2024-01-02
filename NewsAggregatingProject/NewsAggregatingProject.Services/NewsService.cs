using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data.Entities;
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
            if (string.IsNullOrWhiteSpace(sourceRss)) return null;
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

        public async Task<NewsDto> GetNewsByUrl(string url,NewsDto dto)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var textNode = doc.DocumentNode.SelectSingleNode("//div[@class=\"news-text\"]");
            var newsRes = textNode.SelectSingleNode("//div[@class=\"news-reference\"]");
            textNode.RemoveChild(newsRes);
            var textValue = textNode.InnerHtml;
            //var lastInterestedElement = textValue.IndexOf("<div id=\"news-text-end\"></div>");
            //textValue = textValue.Remove(lastInterestedElement);
            dto.ConentNew = textValue;
            return dto;
        }

        public Task<NewsDto> GetNewsByUrl(string Url)
        {
            throw new NotImplementedException();
        }

        public async Task<string[]> GetExistedNewsUrls()
        {
            var existedNewsUrls = await _unitOfWork.NewRepository.GetAsQueryable()
                .Select(news => news.SourceUrl).ToArrayAsync();
            return existedNewsUrls;
        }
        public async Task<int> InsertParsedNews(List<NewsDto> listFullfilledNews)
        {
            var news=listFullfilledNews.Select(dto=>new News()
            {
                Id= dto.Id,
                DataAndTime=dto.Date,
                ContentNew=dto.ConentNew,
                Title=dto.Title,
                Description=dto.Description,
                SourceId=dto.SourceId,
                SourceUrl=dto.SourceUrl
            }).ToArray();
            await _unitOfWork.NewRepository.InsertMany(news);
            return await _unitOfWork.Commit();
        }
    }
}
