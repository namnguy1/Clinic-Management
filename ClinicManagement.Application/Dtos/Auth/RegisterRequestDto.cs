using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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
    }
}