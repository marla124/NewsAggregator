using NewsAggregatingProject.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregatingProject.Services.Interfaces
{
    public interface INewsService
    {
        public Task<NewsDto[]> AggregateDataFromByRssSourceId(Guid Id);
        public Task<string[]> GetExistedNewsUrls();
        public Task<NewsDto> GetNewsByUrl(string Url, NewsDto dto);
        public Task<int> InsertParsedNews(List<NewsDto> listFullfilledNews);
        public Task<NewsDto?> GetNewsById(Guid Id);
        public Task<NewsDto?[]> GetNewsByName(string Name);
        public Task<NewsDto[]?> GetPositive();
        public Task DeleteNews(Guid id);
        public Task CreateNews(NewsDto dto);
        public Task CreateNewsAndSource(NewsDto newsDto, SourceDto sourceDto);
        public Task RateUnratedNews();

    }
}
