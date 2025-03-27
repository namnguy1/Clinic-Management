using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagement.Domain.Entities
{
    public class SpecialtyHospitalOrClinic
    {
        public int SpecialtyId { get; set; }
        public Specialty? Specialty { get; set; }

        public int HospitalOrClinicId { get; set; }
        public HospitalOrClinic? HospitalOrClinic { get; set; }
    }
}