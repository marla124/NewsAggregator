using NewsAggregatingProject.Data.Entities;


namespace NewsAggregatingProject.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<New> NewRepository { get; }
        IRepository<Source> SourceRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        IRepository<RatingScale> RatingScaleRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<UserStatus> UserStatusRepository { get; }
        Task<int> Commit();
    }
}
