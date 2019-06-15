using System;
using System.Collections.Generic;
using REST_API_with_repository_Pattern.Models.Entities;

namespace REST_API_with_repository_Pattern.Repositories
{
    public partial class Todo : BaseEntity
    {
        public string Body { get; set; }
        public bool? Status { get; set; }
        public int? TaskListId { get; set; }

        public virtual TaskList TaskList { get; set; }
    }
}
