using NewsAggregatingProject.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregatingProject.Services.Interfaces
{
    public interface INewsService
    {
        public Task AggregateNewsFromRssBySourceId(Guid sourceId);
        public Task ParseNewsText();
        public Task<NewsDto?> GetNewsById(Guid Id);
        public Task<NewsDto[]?> GetPositive();
        public Task DeleteNews(Guid id);
        public Task CreateNews(NewsDto dto);
        public Task RateBatchOfUnratedNews();
    }
}
