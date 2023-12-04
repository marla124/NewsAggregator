using NewsAggregatingProject.Data.Entities;
using NewsAggregatingProject.Data;
using Microsoft.EntityFrameworkCore;

namespace NewsAggregatingProject.Repositories
{
    public class NewRepository : Repository<New>, INewRepository
    {
        public NewRepository(NewsAggregatingDBContext dBContext) : base(dBContext)
        {

        }

        public async override Task<List<New>> FindBy()
        {
            return await _dbSet.Where(news => !string.IsNullOrEmpty(news.Title)).ToListAsync();
        }

    }
}
