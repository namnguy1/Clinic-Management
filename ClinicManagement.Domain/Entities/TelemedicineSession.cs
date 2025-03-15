using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Domain.Entities
{
    public class TelemedicineSession
    {
        [Key]
        public int SessionId { get; set; }

        public int AppointmentId { get; set; } // Liên kết với lịch hẹn

        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        [Required]
        [MaxLength(500)]
        public string? SessionUrl { get; set; } // URL cuộc gọi video

        [Required]
        [MaxLength(50)]
        public string? SessionStatus { get; set; } // scheduled, active, completed, cancelled

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}