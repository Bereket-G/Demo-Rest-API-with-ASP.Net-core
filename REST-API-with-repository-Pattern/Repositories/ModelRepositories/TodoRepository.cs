using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using REST_API_with_repository_Pattern.Models.Entities;

namespace REST_API_with_repository_Pattern.Repositories
{
    public class TodoRepository : Repository<Todo> , ITodoRepository
    {
        public TodoRepository(DbContext context) : base(context)
        {
        }

    }
}