using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Dtos.Appointment
{
    public class CreateAppointmentRequest
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Description { get; set; } // Tuỳ chọn
    }
}