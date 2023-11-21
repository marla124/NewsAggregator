namespace NewsAggregatingProject.Data.Entities
{
    public class RatingScale:IBaseEntity
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public List<New> News { get; set; }
    }
}
