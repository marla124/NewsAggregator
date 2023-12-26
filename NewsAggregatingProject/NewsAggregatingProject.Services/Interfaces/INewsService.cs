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
    }
}
