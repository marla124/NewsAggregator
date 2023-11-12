using Microsoft.EntityFrameworkCore;
using NewsAggregatingProject.Data.Entities;

namespace NewsAggregatingProject.Data
{
    public class NewsAggregatingDBContext : DbContext
    {
        //private const string ConnString = "Server=DESKTOP-2FD6QEU;DataBase=NewAggregator;TrustedConnection=True;";
        DbSet<Category> Categories { get; set; }
        DbSet<New> News { get; set; }
        DbSet<Source> Sources { get; set; }
        DbSet<RatingScale> RatingScales { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserStatus> UserStatuses { get; set; }

        DbSet<Comment> Comments { get; set; }

        public NewsAggregatingDBContext(DbContextOptions<NewsAggregatingDBContext> options) : base(options) { }
        //public NewsAggregatorDBContext(DbContextOptionsBuilder optionsBuilder) {
        //    optionsBuilder.UseSqlServer(ConnString);    
        //}
    }
}
