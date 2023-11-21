using NewAggregating.Repsitories;
using NewsAggregatingProject.Data;
using NewsAggregatingProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAggregating.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NewsAggregatingDBContext _dbContext;
        private readonly IRepository<New> _newRepository;
        private readonly IRepository<Source> _sourceRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<RatingScale> _ratingScaleRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserStatus> _userStatusRepository;

        public UnitOfWork(NewsAggregatingDBContext dbContext, IRepository<New> newRepository, IRepository<Source> sourceRepository, 
            IRepository<Comment> commentRepository, IRepository<Category> categoryRepository, IRepository<RatingScale> ratingScaleRepository, 
            IRepository<User> userRepository, IRepository<UserStatus> userStatusRepository)
        {
            _dbContext = dbContext;
            _newRepository = newRepository;
            _sourceRepository = sourceRepository;
            _commentRepository = commentRepository;
            _categoryRepository = categoryRepository;
            _ratingScaleRepository = ratingScaleRepository;
            _userRepository = userRepository;
            _userStatusRepository = userStatusRepository;
        }

        public IRepository<New> NewRepository => _newRepository;

        public IRepository<Source> SourceRepository =>_sourceRepository;
        public IRepository<Comment> CommentRepository =>_commentRepository;
        public IRepository<UserStatus> UserStatusRepository =>_userStatusRepository;
        public IRepository<User> UserRepository =>_userRepository;
        public IRepository<Category> CategoryRepository =>_categoryRepository;
        public IRepository<RatingScale> RatingScaleRepository =>_ratingScaleRepository;

        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
