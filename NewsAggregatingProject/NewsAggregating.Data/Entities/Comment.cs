namespace NewsAggregatingProject.Data.Entities
{
    public class Comment:IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid Id_user { get; set; }
        public string Text { get; set; }
        public DateTime DateAndTime { get; set; }
        public Guid NewId { get; set; }
        public New New { get; set; }
        public Guid? ParentCommentId { get; set; }
        public List<Comment> ChildComments { get; set; }
        public Comment ParentComment { get; set; }
        public User User { get; set; }
    }
}
