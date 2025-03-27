using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagement.Domain.Entities
{
    public class Specialty
    {
        public int SpecialtyId { get; set; }
        public string Name { get; set; } = string.Empty;      // Ví dụ: "Da liễu"
        public string Description { get; set; } = string.Empty;
        // Bảng trung gian N-N với Doctor
        public List<SpecialtyDoctor> SpecialtyDoctors { get; set; } = new();

        // Bảng trung gian N-N với HospitalOrClinic
        public List<SpecialtyHospitalOrClinic> SpecialtyHospitalOrClinics { get; set; } = new();
    }
}