using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Application.Dtos.Auth
{
    public class RegisterResponseDto
    {
        [Required]
        public string? FullName { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    }
}