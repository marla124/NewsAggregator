namespace NewsAggregatingProject.Data.Entities
{
    public class User : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Guid UserStatusId { get; set; }
        public UserStatus UserStatus { get; set; }
        public List<Comment> Comments { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
