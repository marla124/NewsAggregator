using MediatR;
using NewsAggregatingProject.Core;

namespace NewsAggregatingProject.Data.CQS.Commands
{
    public class AddNewsCommand: IRequest
    {
        public NewsDto News { get; set; }
    }
}
