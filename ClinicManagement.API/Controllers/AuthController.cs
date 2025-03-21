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
                Role = UserRole.Patient,  // Nếu đăng ký mặc định là Patient
                PhoneNumber = request.PhoneNumber
            };

            try
            {
                var registeredUser = await _authService.RegisterAsync(user, request.Password, request);
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
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
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
