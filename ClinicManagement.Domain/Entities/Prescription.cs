using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagement.Domain.Entities
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }

        public int AppointmentId { get; set; } // Liên kết với cuộc hẹn

        [Required]
        public string MedicationDetails { get; set; } // Chi tiết thuốc, liều lượng

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}