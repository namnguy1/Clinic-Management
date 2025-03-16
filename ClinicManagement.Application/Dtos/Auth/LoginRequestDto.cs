using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Application.Dtos.Auth
{
    public class LoginRequestDto
    {
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}