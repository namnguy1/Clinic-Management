using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Domain.Entities
{
    public class TelemedicineSession
    {
        [Key]
        public int SessionId { get; set; }

        public int AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }

        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        [Required]
        [MaxLength(500)]
        public string? SessionUrl { get; set; } // URL cuộc gọi video

        [Required]
        [MaxLength(50)]
        public TelemedicineSessionStatus? SessionStatus { get; set; } // scheduled, active, completed, cancelled

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}