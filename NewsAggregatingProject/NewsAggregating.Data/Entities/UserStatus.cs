namespace NewsAggregatingProject.Data.Entities
{
    public class UserStatus:IBaseEntity
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public List<User> Users { get; set; }
    }
}
