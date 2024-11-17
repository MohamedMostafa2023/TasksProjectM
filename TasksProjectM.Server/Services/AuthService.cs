using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TasksProjectM.Server.Models;
using TasksProjectM.Server.Repositories;

namespace TasksProjectM.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(UserForRegistration userForRegistration)
        {
            // Check if the user already exists
            var existingUser = await _userRepository.GetByUserNameAsync(userForRegistration.UserName);
            if (existingUser != null)
                throw new Exception("User already exists.");

            var user = new User
            {
                UserName = userForRegistration.UserName,
                Email = userForRegistration.Email,
                Password = userForRegistration.Password // In a real app, hash the password
            };

            await _userRepository.RegisterAsync(user);

            return GenerateJwtToken(user);
        }

        public async Task<string> LoginAsync(UserForLogin userForLogin)
        {
            var user = await _userRepository.LoginAsync(userForLogin.UserName, userForLogin.Password);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials.");

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
