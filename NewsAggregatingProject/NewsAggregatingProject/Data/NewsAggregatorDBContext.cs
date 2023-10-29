using Microsoft.EntityFrameworkCore;
using NewsAggregatorProject.Data.Entities;

namespace NewsAggregatorProject.Data
{
    public class NewsAggregatorDBContext : DbContext
    {
        private const string ConnString = "Server=DESKTOP-2FD6QEU;DataBase=NewAggregator;TrustedConnection=True;";
        DbSet<Category> Categories { get; set; }
        DbSet<New> News { get; set; }
        DbSet<Source> Sources { get; set; }
        DbSet<RatingScale> RatingScales { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserStatus> UserStatuses { get; set; }

        DbSet<Comment> Comments { get; set; }

        public NewsAggregatorDBContext(DbContextOptions<NewsAggregatorDBContext> options) : base(options) {
            
        }
    }
}
