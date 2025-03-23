using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagement.Domain.Entities
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }
        
        // Liên kết với MedicalRecord, vì một bệnh án có thể có nhiều đơn thuốc
        public int MedicalRecordId { get; set; }

        [Required]
        [MaxLength(200)]
        public string MedicineName { get; set; } = string.Empty; // Tên thuốc

        [Required]
        [MaxLength(50)]
        public string Dosage { get; set; } = string.Empty; // Liều lượng thuốc

        [MaxLength(500)]
        public string? Instructions { get; set; } // Hướng dẫn sử dụng thuốc

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation property
        public MedicalRecord? MedicalRecord { get; set; }
    }
}