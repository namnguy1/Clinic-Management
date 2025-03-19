using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Infrastructure.ClinicDbContexts;
using ClinicManagement.Application.Interfaces;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ClinicDbContext _context;
        public async Task AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                throw new InvalidOperationException($"Appointment with ID {id} not found.");
            }
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToListAsync();
        }

        public async Task<Appointment> GetByIdAsync(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
            {
                throw new InvalidOperationException($"Appointment with ID {id} not found.");
            }

            return appointment;
        }

        public Task UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            return _context.SaveChangesAsync();
        }
    }
}