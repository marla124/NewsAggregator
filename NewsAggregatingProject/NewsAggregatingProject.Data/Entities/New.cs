namespace NewsAggregatingProject.Data.Entities
{
    public class New : IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid IdSource { get; set; }
        public Guid IdCategory { get; set; }
        public Guid IdRating { get; set; }
        public string Description { get; set; }
        public string Title{ get; set; }
        public string Titleqwdqq { get; set; }
        public string ContentNew { get; set; }
        public DateTime DataAndTime { get; set; }
        public Source Source { get; set; }
        public RatingScale RatingScale { get; set; }
        public Category Category { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
