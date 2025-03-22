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
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly ClinicDbContext _context;
        public MedicalRecordRepository(ClinicDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(MedicalRecord record)
        {
            await _context.MedicalRecords.AddAsync(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MedicalRecord record)
        {
            _context.MedicalRecords.Remove(record);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MedicalRecord>> GetAllAsync()
        {
            return await _context.MedicalRecords
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .Include(m => m.Prescriptions)
                .ToListAsync();
        }

        public async Task<MedicalRecord> GetByIdAsync(int recordId)
        {
            var record = await _context.MedicalRecords
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .Include(m => m.Prescriptions)
                .FirstOrDefaultAsync(m => m.RecordId == recordId) ?? throw new InvalidOperationException($"Medical record with ID {recordId} not found.");
            return record;
        }

        public async Task<IEnumerable<MedicalRecord>> GetByPatientIdAsync(int patientId)
        {
            return await _context.MedicalRecords
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .Include(m => m.Prescriptions)
                .Where(m => m.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<MedicalRecord>> GetByDoctorIdAsync(int doctorId)
        {
            return await _context.MedicalRecords
                .Include(m => m.Patient)
                .Include(m => m.Doctor)
                .Include(m => m.Prescriptions)
                .Where(m => m.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task UpdateAsync(MedicalRecord record)
        {
            _context.MedicalRecords.Update(record);
            await _context.SaveChangesAsync();
        }
    }
}