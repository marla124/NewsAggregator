using MediatR;


namespace NewsAggregatingProject.Data.CQS.Queries
{
    public class GetUnratedNewsQuery : IRequest<Guid[]>
    {

    }
}
