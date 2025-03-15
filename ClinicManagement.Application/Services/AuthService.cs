using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Application.Interfaces;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        // Có thể inject các dịch vụ khác như IPasswordHasher, ITokenService,...

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> RegisterAsync(User user, string password)
        {
            // Xử lý hash password
            user.PasswordHash = HashPassword(password);
            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
                throw new Exception("Invalid credentials");
            // Tạo token JWT
            string token = GenerateToken(user);
            return token;
        }

        private string HashPassword(string password)
        {
            // Ví dụ: sử dụng BCrypt, hoặc các phương pháp hash khác.
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        private string GenerateToken(User user)
        {
            // Triển khai tạo JWT token dựa trên user info
            // (Có thể dùng package Microsoft.IdentityModel.Tokens và System.IdentityModel.Tokens.Jwt)
            return "generated-jwt-token";
        }
    }
}