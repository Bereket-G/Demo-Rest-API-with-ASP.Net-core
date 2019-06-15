using System.Threading.Tasks;
using REST_API_with_repository_Pattern.Repositories;

namespace REST_API_with_repository_Pattern.Auth
{
    public interface IAuthService
    {
        User Login(string username, string password);
        User Register(User user, string password);
        bool UserExists(string username);
    }
}