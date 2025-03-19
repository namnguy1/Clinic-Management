using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);
        Task<Appointment> GetAppointmentAsync(int id);
        Task<List<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(int id);
    }
}