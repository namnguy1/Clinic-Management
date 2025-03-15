using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Domain.Entities
{
    public class MedicalRecord
    {
        [Key]
        public int RecordId { get; set; }

        public int AppointmentId { get; set; } // Liên kết với lịch hẹn

        [Required]
        public string? Diagnosis { get; set; } // Chẩn đoán bệnh

        public string? TreatmentPlan { get; set; } // Kế hoạch điều trị

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}