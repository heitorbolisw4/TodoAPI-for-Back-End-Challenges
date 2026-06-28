using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Todo.Entities;
using Todo.Interface;
using Todo.Jwt;

namespace Todo.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IConfiguration config, IOptions<JwtSettings> jwtOptions)
        {
            _config = config;
            _jwtSettings = jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            var secretKey = _jwtSettings.SecretKey;
            var issuer = _jwtSettings.Issuer;
            var audience = _jwtSettings.Audience;
            var expirationInMinutes = _jwtSettings.ExpirationInMinutes;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Mail)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

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