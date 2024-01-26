using MediatR;
using NewsAggregatingProject.Core;

namespace NewsAggregatingProject.Data.CQS.Commands
{
    public class InsertRssDataCommand : IRequest
    {
        public NewsDto[] News { get; set; }
    }
}
