namespace NewsAggregatingProject.Data.Entities
{
    public class Source : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public List<News> News { get; set; }
    }
}
