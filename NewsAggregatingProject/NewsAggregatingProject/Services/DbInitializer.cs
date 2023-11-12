using NewsAggregatingProject.Data;

namespace NewsAggregatingProject.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly NewsAggregatingDBContext _context;

        public DbInitializer(NewsAggregatingDBContext context)
        {
            _context = context;
        }

        public async Task InitDbWithDefaultValues()
        {

        }
    }
}
