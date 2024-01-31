using MediatR;
using NewsAggregatingProject.Core;

namespace NewsAggregatingProject.Data.CQS.Commands
{
    public class AddRefreshTokenCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Ip { get; set; }
    }
}
