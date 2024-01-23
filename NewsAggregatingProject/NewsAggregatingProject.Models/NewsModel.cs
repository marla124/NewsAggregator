namespace NewsAggregatingProject.Models
{
    public class NewsModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }
        public string Title { get; set; }

        public string ConentNew { get; set; }
        public DateTime Date { get; set; }
        public float? Rate { get; set; }
        public string SourceUrl { get; set; }
        public Guid SourceId { get; set; }
    }
}
