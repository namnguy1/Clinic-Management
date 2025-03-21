using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Application.Dtos.Auth
{
    public class RegisterRequestDto
    {
        [Required]
        public string? FullName { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required, MinLength(6)]
        public string? Password { get; set; }

        public string? PhoneNumber { get; set; }

         [Required]
        [MaxLength(50)]
        public string? InsuranceNumber { get; set; } // Số bảo hiểm y tế (nếu có)

        public DateTime DateOfBirth { get; set; } // Ngày sinh

        [Required]
        public GenderEnum? Gender { get; set; } // Male, Female, Other

        [MaxLength(255)]
        public string? Address { get; set; } // Địa chỉ của bệnh nhân
    }
}