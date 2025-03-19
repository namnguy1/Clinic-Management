using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Application.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetByIdAsync(int id);
        Task<List<Appointment>> GetAllAsync();
        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(int id);
    }
}