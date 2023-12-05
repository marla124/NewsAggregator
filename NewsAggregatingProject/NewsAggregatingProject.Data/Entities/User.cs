namespace NewsAggregatingProject.Data.Entities
{
    public class User : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public Guid UserStatusId { get; set; }
        public UserStatus UserStatus { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
