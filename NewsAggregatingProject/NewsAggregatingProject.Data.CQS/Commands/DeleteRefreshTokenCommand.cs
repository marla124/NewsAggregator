using MediatR;

namespace WebApp.Data.CQS.Commands;

public class DeleteRefreshTokenCommand : IRequest
{
    public Guid Id { get; set; }
}
