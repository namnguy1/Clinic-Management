using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ClinicManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagement.Domain.Entities
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        // Liên kết đúng với Patient
        public int PatientId { get; set; }

        // Liên kết đúng với Doctor
        public int DoctorId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; } // Mô tả triệu chứng hoặc lý do khám

        [Required]
        public AppointmentStatus Status { get; set; } // scheduled, completed, cancelled

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public Doctor? Doctor { get; set; }
        
        public Patient? Patient { get; set; }
    }
}