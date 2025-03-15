using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Domain.Entities
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        // Liên kết với bệnh nhân và bác sĩ
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [MaxLength(500)]
        public string Description { get; set; } // Mô tả triệu chứng hoặc lý do khám

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } // scheduled, completed, cancelled

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}