using Todo.Entities;

namespace Todo.Interface
{
    public interface IAuthService
    {
        public string HashGeneration(string passwd);
        public bool PasswVerify(string passwdHash, string inputPasswd);
        public string GenerateToken(User user);
    }
}