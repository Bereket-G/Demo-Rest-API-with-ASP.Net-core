using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using REST_API_with_repository_Pattern.Repositories;

namespace REST_API_with_repository_Pattern.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User Login(string username, string password)
        {
            var user = _unitOfWork.Users.GetSingleOrDefault((x) => x.UserName == username);
            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null; // I should return un authorized response

            return user; // auth successful
        }

        private bool VerifyPasswordHash(string password, string passwordHash, string salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(Encoding.ASCII.GetBytes(salt)))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }

            return true;
        }

        private void CreatePasswordHash(string password, out string passwordHash, out string salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                salt = System.Text.Encoding.UTF8.GetString(hmac.Key);
                passwordHash =
                    System.Text.Encoding.UTF8.GetString(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }

        public User Register(User user, string password)
        {
            string passwordHash, salt;
            CreatePasswordHash(password, out passwordHash, out salt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = salt;

            _unitOfWork.Users.Add(user);

            if (_unitOfWork.Complete() == 1)
            {
                return user;
            }

            return null;
        }

        public bool UserExists(string username)
        {
            if (_unitOfWork.Users.Find(x => x.UserName == username).Any())
                return true;
            return false;
        }
    }
}