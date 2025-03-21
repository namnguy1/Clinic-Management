using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Application.Interfaces;
using ClinicManagement.Infrastructure.ClinicDbContexts;

namespace ClinicManagement.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ClinicDbContext _context;
        public PatientRepository(ClinicDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Patient patient)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public Task<Patient> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Patient> GetByIdAsync(int PatientId)
        {
            var patient = await _context.Patients.FindAsync(PatientId);
            if (patient == null)
            {
                throw new InvalidOperationException($"User with ID {patient} not found.");
            }
            return patient;
        }

        public Task UpdateAsync(Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}