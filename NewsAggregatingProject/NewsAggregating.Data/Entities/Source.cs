namespace NewsAggregatingProject.Data.Entities
{
    public class Source
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public List<New> News { get; set; }
    }
}
