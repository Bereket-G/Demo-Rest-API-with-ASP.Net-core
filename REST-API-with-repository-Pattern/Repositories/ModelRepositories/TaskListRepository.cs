using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using REST_API_with_repository_Pattern.Models.Entities;

namespace REST_API_with_repository_Pattern.Repositories
{
    public class TaskListRepository : Repository<TaskList> , ITaskListRepository
    {
        public TaskListRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Todo> GetAllTasks()
        {
            throw new System.NotImplementedException();
        }
    }
}