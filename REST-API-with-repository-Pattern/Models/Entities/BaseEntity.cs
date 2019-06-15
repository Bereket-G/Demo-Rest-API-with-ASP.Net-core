using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace REST_API_with_repository_Pattern.Models.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Deleted { get; set; }
    }
}
