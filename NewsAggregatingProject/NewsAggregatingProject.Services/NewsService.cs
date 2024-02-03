using HtmlAgilityPack;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using NewsAggregatingProject.API.Mappers;
using NewsAggregatingProject.Core;
using NewsAggregatingProject.Data.CQS.Commands;
using NewsAggregatingProject.Data.CQS.Queries;
using NewsAggregatingProject.Data.Entities;
using NewsAggregatingProject.Repositories;
using NewsAggregatingProject.Services.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;

namespace NewsAggregatingProject.Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly NewsMapper _newsMapper;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;



        public NewsService(IUnitOfWork unitOfWork, NewsMapper newsMapper, IMediator mediator, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _newsMapper=newsMapper;
            _mediator=mediator;
            _configuration=configuration;
        }
        private async Task<NewsDto[]?> AggregateDataFromByRssSourceId(Guid sourceId)
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
                    ConentNew = item.Summary.Text,
                    SourceUrl = item.Id
                }).ToArray();

                return rssNews;
            }

        }

        private (Guid,string) GetNewsByUrl((Guid, string) newsInfo)
        {
            var web = new HtmlWeb();
            var doc = web.Load(newsInfo.Item2);

            var textNode = doc.DocumentNode.SelectSingleNode("//div[@class=\"news-text\"]");
            var newsRes = textNode.SelectSingleNode("//div[@class=\"news-reference\"]");
            textNode.RemoveChild(newsRes);
            var textValue = textNode.InnerHtml;
            var tuple=(newsInfo.Item1,newsInfo.Item2);
            
            return tuple;
        }


        private async Task<string[]> GetExistedNewsUrls()
        {
            var existedNewsUrls = await _unitOfWork.NewsRepository.GetAsQueryable()
                .Select(news => news.SourceUrl).ToArrayAsync();
            return existedNewsUrls;
        }
        public async Task ParseNewsText()
        {
            var newsWithoutText = await _mediator.Send(new GetNewsWithoutTextQuery()); 
            var data=newsWithoutText.Select(tuple => GetNewsByUrl(tuple)).ToDictionary(tuple=>tuple.Item1, tuple=>tuple.Item2);

            
            await _mediator.Send(new UpdateNewsText() { NewsData = data });
        }

        public async Task<NewsDto?> GetNewsById(Guid id)
        {
            var newsDto = _newsMapper.NewsToNewsDto(
                await _mediator.Send(new GetNewsByIdQuery { Id = id }));
            return newsDto;

        }

        public async Task<NewsDto?[]> GetNewsByName(string name)
        {
            var news = await _unitOfWork.NewsRepository
                .FindBy(news => news.Title.Contains(name))
                .Select(news => _newsMapper.NewsToNewsDto(news)).ToArrayAsync();
            return news;
        }

        public async Task<NewsDto[]?> GetPositive()
        {
            var news = await _unitOfWork.NewsRepository
            .GetAsQueryable()
            .Select(onenew => _newsMapper.NewsToNewsDto(onenew))
            .ToArrayAsync();
            return news;
        }

        public async Task DeleteNews(Guid id)
        {
            await _unitOfWork.NewsRepository.DeleteById(id);
            await _unitOfWork.Commit();
        }

        public async Task CreateNews(NewsDto dto)
        {
            var command=new AddNewsCommand() { News = dto };
            _mediator.Send(command);
        }

        public Task CreateNewsAndSource(NewsDto newsDto, SourceDto sourceDto)
        {
            throw new NotImplementedException();
        }

        public async Task RateBatchOfUnratedNews()
        {
            var ids=await _mediator.Send(new GetUnratedNewsQuery());
            foreach(var id in ids)
            {
                await Rate(id);
            }
        }
        private async Task Rate(Guid id)
        {
            var text = await _mediator.Send(new GetNewsTextByIdQuery() { Id = id });
            var dictionary = _configuration
                .GetSection("Dictionary")
                .GetChildren()
                .ToDictionary(section=>section.Key, 
                    section=>Convert.ToInt32(section.Value));
            
            var textWithoutHtml = RemoveHTMLTags(text);

            var lemmas = await GetLemmas(textWithoutHtml);
            int? rate = null;

            foreach (var lemma in lemmas) 
            { 
                if (dictionary.TryGetValue(lemma, out int value))
                {
                    if(rate==null) rate= value;
                    rate += value;
                }
            }
            await _mediator.Send(new SetNewsRateCommand() { Id = id, Rate = rate });
        }

        private string RemoveHTMLTags(string html)
        {
            return Regex.Replace(html, "<.*?>", string.Empty);
        }
        private async Task<string[]> GetLemmas(string text)
        {
            using(var httpClient=new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey={_configuration["AppSetting:IsprasKey"]}");

                request.Headers.Add("Accept", "application/json");

                request.Content = JsonContent.Create(new[]
                {
                    new { Text=text}
                });
                var response= await httpClient.SendAsync(request);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var lemmas = JsonConvert.DeserializeObject<IsprasLemmatizationResponse[]>(responseString)?
                        .FirstOrDefault()?
                        .Annotations
                        .Lemma
                        .Select(l => l.Value)
                        .ToArray();
                    return lemmas;
                }
                return new string[] { };

            }
        }

        public async Task AggregateNewsFromRssBySourceId(Guid sourceId)
        {
            var data = await AggregateDataFromByRssSourceId(sourceId);

            
            var existedNews = await GetExistedNewsUrls();
            var uniqueNews = data
                .Where(dto => !existedNews
                .Any(url => dto.SourceUrl.Equals(url)))
                .ToArray();
            var command = new InsertRssDataCommand()
            {
                News = uniqueNews
            };
            await _mediator.Send(command);

        }
    }
}
