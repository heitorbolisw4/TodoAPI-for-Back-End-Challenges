using Todo.Entities;
using Todo.Interface;

namespace Todo.Service
{
    public class AuthService : IAuthService
    {
        public string GenerateToken(User user)
        {
            throw new NotImplementedException();
        }

        public string HashGeneration(string passwd)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(passwd);
            return hash;
        }

        public bool PasswVerify(string passwdHash, string inputPasswd)
        {
            return BCrypt.Net.BCrypt.Verify(inputPasswd, passwdHash);
        }
    }
}