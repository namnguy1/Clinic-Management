using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagement.Domain.Entities
{
    public class DoctorHospitalOrClinic
    {
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public int HospitalOrClinicId { get; set; }
        public HospitalOrClinic? HospitalOrClinic { get; set; }
    }
}