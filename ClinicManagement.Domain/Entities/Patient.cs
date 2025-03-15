using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagement.Domain.Entities
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; } // Liên kết với bảng Users

        [Required]
        [MaxLength(50)]
        public string? InsuranceNumber { get; set; } // Số bảo hiểm y tế (nếu có)

        public DateTime DateOfBirth { get; set; } // Ngày sinh

        [Required]
        [MaxLength(10)]
        public string? Gender { get; set; } // Male, Female, Other

        [MaxLength(255)]
        public string? Address { get; set; } // Địa chỉ của bệnh nhân

        public User? User { get; set; } // Navigation property
    }
}