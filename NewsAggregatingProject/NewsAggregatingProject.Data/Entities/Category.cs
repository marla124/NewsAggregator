namespace NewsAggregatingProject.Data.Entities
{
    public class Category : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<News> News { get; set; }
    }
}
