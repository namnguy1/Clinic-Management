using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Application.Interfaces;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Infrastructure.ClinicDbContexts;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Infrastructure.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly ClinicDbContext _context;

        public PrescriptionRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<Prescription> AddAsync(Prescription prescription)
        {
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
            return prescription;
        }

        public async Task DeleteAsync(Prescription prescription)
        {
            _context.Prescriptions.Remove(prescription);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Prescription>> GetAllAsync()
        {
            return await _context.Prescriptions.ToListAsync();
        }

        public async Task<Prescription> GetByIdAsync(int id)
        {
            var prescription = await _context.Prescriptions
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);

            if (prescription == null)
            {
                throw new KeyNotFoundException($"Prescription with ID {id} was not found.");
            }

            return prescription;
        }

        public async Task<IEnumerable<Prescription>> GetByMedicalRecordIdAsync(int recordId)
        {
            return await _context.Prescriptions
                .Where(p => p.MedicalRecordId == recordId)
                .ToListAsync();
        }

        public async Task<Prescription> UpdateAsync(Prescription prescription)
        {
            _context.Prescriptions.Update(prescription);
            await _context.SaveChangesAsync();
            return prescription;
        }
    }
}