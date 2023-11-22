using NewsAggregatingProject.Data;
using NewsAggregatingProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using NewAggregating.Repositories;

namespace NewAggregating.Repsitories
{
    public class NewRepository: Repository<New>, INewRepository
    {
        public NewRepository(NewsAggregatingDBContext dBContext) : base(dBContext)
        { 

        }

        public async override Task<List<New>> FindBy()
        {
            return await _dbSet.Where(news=>!string.IsNullOrEmpty(news.Title)).ToListAsync();
        }

    }
}