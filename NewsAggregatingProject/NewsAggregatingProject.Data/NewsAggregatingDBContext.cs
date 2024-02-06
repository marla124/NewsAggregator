using Microsoft.EntityFrameworkCore;
using NewsAggregatingProject.Data.Entities;

namespace NewsAggregatingProject.Data
{
    public class NewsAggregatingDBContext : DbContext
    {
        public DbSet<News> News { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        public NewsAggregatingDBContext(DbContextOptions<NewsAggregatingDBContext> options) : base(options) { }

    }
}
