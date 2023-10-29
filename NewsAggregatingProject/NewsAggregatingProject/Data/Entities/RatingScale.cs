namespace NewsAggregatorProject.Data.Entities
{
    public class RatingScale
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public List<New> News { get; set; }
    }
}
