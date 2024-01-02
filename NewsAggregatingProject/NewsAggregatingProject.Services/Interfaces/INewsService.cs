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
        public Task<NewsDto> GetNewsByUrl(string Url);
        public Task<string[]> GetExistedNewsUrls();
        public Task<NewsDto> GetNewsByUrl(string Url, NewsDto dto);
        public Task<int> InsertParsedNews(List<NewsDto> listFullfilledNews);
    }
}
