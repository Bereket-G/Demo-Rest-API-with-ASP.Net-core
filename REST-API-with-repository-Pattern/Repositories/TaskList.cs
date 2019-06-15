using System;
using System.Collections.Generic;
using REST_API_with_repository_Pattern.Models.Entities;

namespace REST_API_with_repository_Pattern.Repositories
{
    public partial class TaskList : BaseEntity
    {
        public TaskList()
        {
            Todo = new HashSet<Todo>();
        }

        public string Title { get; set; }

        public virtual ICollection<Todo> Todo { get; set; }
    }
}
