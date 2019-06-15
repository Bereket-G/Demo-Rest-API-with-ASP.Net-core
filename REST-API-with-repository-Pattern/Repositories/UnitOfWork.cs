namespace REST_API_with_repository_Pattern.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyNewAppContext _context;

        public ITodoRepository Todos { get; }
        public ITaskListRepository TaskLists { get; }

        public IUserRepository Users { get; }


        public UnitOfWork(MyNewAppContext context)
        {
            _context = context;
            Todos = new TodoRepository(_context);
            TaskLists = new TaskListRepository(_context);
            Users = new UserRepository(_context);
        }


        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}