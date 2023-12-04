namespace NewsAggregatingProject.Data.Entities
{
    public class RatingScale : IBaseEntity
    {
        public Guid Id { get; set; }
        public float? Status { get; set; }
        public List<New> News { get; set; }
    }
}
