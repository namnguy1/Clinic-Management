using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        
        [Required, MaxLength(255)]
        public string? FullName { get; set; }
        
        [Required, EmailAddress, MaxLength(255)]
        public string? Email { get; set; }
        
        [Required]
        public string? PasswordHash { get; set; }
        
        [Required]
        public UserRole? Role { get; set; } // patient, doctor, staff
        
        [Required, MaxLength(20)]
        public string? PhoneNumber { get; set; }
        
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public Doctor? Doctor { get; set; }
        
        public Patient? Patient { get; set; }
    }
}