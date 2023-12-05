using NewsAggregatingProject.Data.Entities;


namespace NewsAggregatingProject.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<News> NewRepository { get; }
        IRepository<Source> SourceRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        IRepository<RatingScale> RatingScaleRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<UserStatus> UserStatusRepository { get; }
        Task<int> Commit();
    }
}
