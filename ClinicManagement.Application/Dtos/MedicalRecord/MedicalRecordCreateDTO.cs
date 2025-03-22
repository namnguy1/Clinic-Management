using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Dtos.MedicalRecord
{
    public class MedicalRecordCreateDTO
    {
        public int PatientId { get; set; }

        public int DoctorId { get; set; }
        public string? Diagnosis { get; set; }
        public string? TreatmentPlan { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}