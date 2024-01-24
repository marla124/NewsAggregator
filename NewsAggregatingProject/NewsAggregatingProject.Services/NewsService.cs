using HtmlAgilityPack;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAggregatingProject.API.Mappers;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data.CQS.Commands;
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
        private readonly NewsMapper _newsMapper;
        private readonly IMediator _mediator;



        public NewsService(IUnitOfWork unitOfWork, NewsMapper newsMapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _newsMapper=newsMapper;
            _mediator=mediator;
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
                    SourceUrl = item.Id
                }).ToArray();

                return rssNews;
            }

        }

        public async Task<NewsDto> GetNewsByUrl(string url, NewsDto dto)
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


        public async Task<string[]> GetExistedNewsUrls()
        {
            var existedNewsUrls = await _unitOfWork.NewRepository.GetAsQueryable()
                .Select(news => news.SourceUrl).ToArrayAsync();
            return existedNewsUrls;
        }
        public async Task<int> InsertParsedNews(List<NewsDto> listFullfilledNews)
        {
            var news = listFullfilledNews.Select(dto => _newsMapper.NewsDtoToNews(dto)).ToArray();
            await _unitOfWork.NewRepository.InsertMany(news);
            return await _unitOfWork.Commit();
        }

        public async Task<NewsDto?> GetNewsById(Guid id)
        {
            var news = (await _unitOfWork.NewRepository.GetById(id));
            if(news == null)
            {
                return null;
            }
            return _newsMapper.NewsToNewsDto(news);
            
        }

        public async Task<NewsDto?[]> GetNewsByName(string name)
        {
            var news = await _unitOfWork.NewRepository
                .FindBy(news => news.Title.Contains(name))
                .Select(news => _newsMapper.NewsToNewsDto(news)).ToArrayAsync();
            return news;
        }

        public async Task<NewsDto[]?> GetPositive()
        {
            var news = await _unitOfWork.NewRepository.GetAsQueryable()
            //.FindBy(news => news.Rate >= 0)
            .Select(news => _newsMapper.NewsToNewsDto(news))
            .ToArrayAsync();
            return news;
        }

        public async Task DeleteNews(Guid id)
        {
            await _unitOfWork.NewRepository.DeleteById(id);
            await _unitOfWork.Commit();
        }

        public async Task CreateNews(NewsDto dto)
        {
            var command=new AddNewsCommand() { News = dto };
            _mediator.Send(command);
        }

        public Task CreateNewsAndSource(NewsDto newsDto, SourceDto sourceDto)
        {
            
        }
    }
}
