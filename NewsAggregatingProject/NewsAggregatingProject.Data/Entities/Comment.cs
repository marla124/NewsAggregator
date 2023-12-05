namespace NewsAggregatingProject.Data.Entities
{
    public class Comment : IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public DateTime DateAndTime { get; set; }
        public Guid NewsId { get; set; }
        public News News { get; set; }
        public Guid? ParentCommentId { get; set; }
        public List<Comment> ChildComments { get; set; }
        public Comment ParentComment { get; set; }
        public User User { get; set; }
    }
}
