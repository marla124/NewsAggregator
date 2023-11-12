namespace NewsAggregatingProject.Data.Entities
{
    public class New
    {
        public Guid Id { get; set; }
        public Guid Id_source { get; set; }
        public Guid Id_category { get; set; }
        public Guid Id_characteristic { get; set; }
        public string ContentNew { get; set; }
        public DateTime DataAndTime { get; set; }
        public Source Source { get; set; }  
        public RatingScale RatingScale { get; set; }
        public Category Category { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
