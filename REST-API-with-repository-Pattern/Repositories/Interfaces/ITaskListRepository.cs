using System.Collections.Generic;
using System.Threading.Tasks;
using REST_API_with_repository_Pattern.Models.Entities;

namespace REST_API_with_repository_Pattern.Repositories
{
    public interface ITaskListRepository : IRepository<TaskList>
    {
        IEnumerable<Todo> GetAllTasks();
    }
}