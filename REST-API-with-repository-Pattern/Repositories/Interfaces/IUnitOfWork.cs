namespace REST_API_with_repository_Pattern.Repositories
{
    public interface IUnitOfWork
    {
        ITodoRepository Todos { get; }
        ITaskListRepository TaskLists { get; }
        IUserRepository Users { get; }
        int Complete();
    }
}