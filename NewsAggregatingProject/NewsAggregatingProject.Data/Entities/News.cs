namespace NewsAggregatingProject.Data.Entities
{
    public class News : IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }
        //public Guid CategoryId { get; set; }
        //public Guid RatingScaleId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string ContentNew { get; set; }
        public DateTime DataAndTime { get; set; }
        public Source Source { get; set; }
        //public RatingScale RatingScale { get; set; }
        //public Category Category { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
