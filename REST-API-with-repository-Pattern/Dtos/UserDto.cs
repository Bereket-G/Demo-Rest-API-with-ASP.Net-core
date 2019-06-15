using REST_API_with_repository_Pattern.Models.Entities;

namespace REST_API_with_repository_Pattern.Dtos
{
    public class UserDto : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}