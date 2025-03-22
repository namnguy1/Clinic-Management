using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Application.Interfaces
{
    public interface IMedicalRecordRepository
    {
        Task<MedicalRecord> GetByIdAsync(int recordId);
        Task<IEnumerable<MedicalRecord>> GetAllAsync();
        Task<IEnumerable<MedicalRecord>> GetByPatientIdAsync(int patientId);
        Task<IEnumerable<MedicalRecord>> GetByDoctorIdAsync(int doctorId);
        Task AddAsync(MedicalRecord record);
        Task UpdateAsync(MedicalRecord record);
        Task DeleteAsync(MedicalRecord record);
    }
}