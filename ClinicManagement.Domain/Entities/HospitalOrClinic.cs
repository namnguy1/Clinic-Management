using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagement.Domain.Entities
{
    public class HospitalOrClinic
    {
        public int HospitalOrClinicId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsHospital { get; set; } // true nếu là bệnh viện, false nếu là phòng khám

        // Quan hệ nhiều-nhiều với Specialty
        public List<SpecialtyHospitalOrClinic> SpecialtyHospitalOrClinics { get; set; } = new();

        // Một bệnh viện/phòng khám có thể có nhiều bác sĩ
        public List<DoctorHospitalOrClinic> DoctorHospitalOrClinics { get; set; } = new();
    }
}