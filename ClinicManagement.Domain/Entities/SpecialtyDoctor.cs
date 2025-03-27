using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagement.Domain.Entities
{
    public class SpecialtyDoctor
    {
        public int SpecialtyId { get; set; }
        public Specialty? Specialty { get; set; }

        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

    }
}