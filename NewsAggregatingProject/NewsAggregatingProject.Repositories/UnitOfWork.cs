﻿using NewsAggregatingProject.Data;
using NewsAggregatingProject.Data.Entities;
namespace NewsAggregatingProject.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NewsAggregatingDBContext _dbContext;
        private readonly IRepository<News> _newRepository;
        private readonly IRepository<Source> _sourceRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserStatus> _userStatusRepository;

        public UnitOfWork(NewsAggregatingDBContext dbContext, IRepository<News> newRepository, IRepository<Source> sourceRepository,
            IRepository<Comment> commentRepository, IRepository<Category> categoryRepository,
            IRepository<User> userRepository, IRepository<UserStatus> userStatusRepository)
        {
            _dbContext = dbContext;
            _newRepository = newRepository;
            _sourceRepository = sourceRepository;
            _commentRepository = commentRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _userStatusRepository = userStatusRepository;
        }

        public IRepository<News> NewsRepository => _newRepository;

        public IRepository<Source> SourceRepository => _sourceRepository;
        public IRepository<Comment> CommentRepository => _commentRepository;
        public IRepository<UserStatus> UserStatusRepository => _userStatusRepository;
        public IRepository<User> UserRepository => _userRepository;
        public IRepository<Category> CategoryRepository => _categoryRepository;

        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
