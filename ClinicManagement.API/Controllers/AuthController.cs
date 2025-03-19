using System.Threading.Tasks;
using ClinicManagement.Application.Interfaces;
using ClinicManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ClinicManagement.Application.Dtos.Auth;
using ClinicManagement.Domain.Enums;
namespace ClinicManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto? request)
        {
            // Kiểm tra null và các trường bắt buộc
            if (request == null ||
                string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid request data.");
            }

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Role = UserRole.Patient,
                PhoneNumber = request.PhoneNumber
            };

            // Gọi service để xử lý đăng ký
            var registeredUser = await _authService.RegisterAsync(user, request.Password);
            
            // Nếu service trả về null, có thể nghĩa là email đã tồn tại hoặc lỗi khác
            if (registeredUser == null)
            {
                return BadRequest("Registration failed. Email might be already in use.");
            }
            var response = new RegisterResponseDto
            {
                FullName = registeredUser.FullName,
                Email = registeredUser.Email,
                PhoneNumber = registeredUser.PhoneNumber
            };
            // Trả về RegisterResponse
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto? request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid email or password.");
            }

            var token = await _authService.LoginAsync(request.Email, request.Password);
            return Ok(new { Token = token });
        }
    }

}
