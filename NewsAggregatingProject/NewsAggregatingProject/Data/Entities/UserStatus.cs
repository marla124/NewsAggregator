namespace NewsAggregatorProject.Data.Entities
{
    public class UserStatus
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public List<User> Users { get; set; }
    }
}
