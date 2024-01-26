using MediatR;
using NewsAggregatingProject.Core;

namespace NewsAggregatingProject.Data.CQS.Commands
{
    public class UpdateNewsText : IRequest 
    {
        public Dictionary<Guid, string> NewsData { get; set; }
    }
}
