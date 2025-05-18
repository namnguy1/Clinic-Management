using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ClinicManagement.Application.Interfaces;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Application.Dtos.Auth;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        private readonly IPatientRepository _patientRepository;
        // Có thể inject các dịch vụ khác như IPasswordHasher, ITokenService,...

        public AuthService(IUserRepository userRepository, IPatientRepository patientRepository,IConfiguration configuration)
        {
            _userRepository = userRepository;
            _patientRepository = patientRepository;
            _configuration = configuration;
        }

        public async Task<User> RegisterAsync(User user, string password, RegisterRequestDto additionalInfo)
        {
            // Hash mật khẩu
            user.PasswordHash = HashPassword(password);
            user.DateCreated = DateTime.UtcNow;

            // Lưu User vào DB
            await _userRepository.AddAsync(user);

            // Nếu role là Patient, tạo thêm record trong bảng Patient
            if (user.Role == UserRole.Patient)
            {
                var patient = new Patient
                {
                    UserId = user.UserId,
                    InsuranceNumber = additionalInfo.InsuranceNumber,
                    DateOfBirth = additionalInfo.DateOfBirth,
                    Gender = additionalInfo.Gender ?? throw new ArgumentNullException(nameof(additionalInfo.Gender), "Gender cannot be null"), // Ensure Gender is not null
                    Address = additionalInfo.Address
                };

                await _patientRepository.AddAsync(patient); // Giả sử bạn inject IPatientRepository
            }

            return user;
        }


        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || user.PasswordHash == null || !VerifyPassword(password, user.PasswordHash))
                throw new Exception("Invalid credentials");
            // Tạo token JWT
            string token = GenerateToken(user);
            return token;
        }

        public async Task<User> GetOrCreateGoogleUserAsync(string email, string name)
        {
            try
            {
                // Tìm user theo email
                var user = await _userRepository.GetByEmailAsync(email);
                if (user != null)
                    return user;

                // Tạo user mới nếu chưa tồn tại
                user = new User
                {
                    Email = email,
                    FullName = name,
                    Role = UserRole.Patient, // Mặc định là Patient
                    DateCreated = DateTime.UtcNow
                };

                await _userRepository.AddAsync(user);
                return user;
            }
            catch (InvalidOperationException)
            {
                // User không tồn tại, tạo mới
                var user = new User
                {
                    Email = email,
                    FullName = name,
                    Role = UserRole.Patient,
                    DateCreated = DateTime.UtcNow
                };

                await _userRepository.AddAsync(user);
                return user;
            }
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            return GenerateToken(user);
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
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured")));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, user.FullName ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role?.ToString() ?? string.Empty)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? "clinic-management-api",
                audience: _configuration["Jwt:Audience"] ?? "clinic-management-client",
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}