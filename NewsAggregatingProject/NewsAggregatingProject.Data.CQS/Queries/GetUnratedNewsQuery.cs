using MediatR;


namespace NewsAggregatingProject.Data.CQS.Queries
{
    public class GetUnratedNewsQuery : IRequest<Guid[]>
    {
        public int MaxTake {  get; set; }


        public GetUnratedNewsQuery(int maxTake=25) 
        {
            MaxTake = maxTake;
        }
    }
}
