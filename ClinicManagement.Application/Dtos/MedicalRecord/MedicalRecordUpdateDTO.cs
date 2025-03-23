using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Dtos.MedicalRecord
{
    public class MedicalRecordUpdateDTO
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int RecordId { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string TreatmentPlan { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}