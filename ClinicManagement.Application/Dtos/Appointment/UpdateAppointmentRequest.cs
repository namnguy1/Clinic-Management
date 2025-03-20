using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.Application.Dtos.Appointment
{
    public class UpdateAppointmentRequest
    {
        public DateTime AppointmentDate { get; set; }
        public string? Description { get; set; }
        public AppointmentStatus Status { get; set; } // "Pending", "Confirmed", ...
    }
}