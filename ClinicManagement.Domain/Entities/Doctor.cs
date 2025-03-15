using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagement.Domain.Entities
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; } // Liên kết với bảng Users

        [Required]
        [MaxLength(100)]
        public string? Specialization { get; set; } // Chuyên môn (Tim mạch, Nội tiết,...)

        [Required]
        [MaxLength(20)]
        public string? LicenseNumber { get; set; } // Số giấy phép hành nghề

        [MaxLength(255)]
        public string? ClinicAddress { get; set; } // Địa chỉ phòng khám (nếu có)

        public User? User { get; set; } // Navigation property
    }
}