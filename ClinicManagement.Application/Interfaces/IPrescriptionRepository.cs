using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Application.Interfaces
{
    public interface IPrescriptionRepository
    {
        Task<Prescription> GetByIdAsync(int id);
        Task<IEnumerable<Prescription>> GetAllAsync();
        Task<IEnumerable<Prescription>> GetByMedicalRecordIdAsync(int recordId);
        Task<Prescription> AddAsync(Prescription prescription);
        Task<Prescription> UpdateAsync(Prescription prescription);
        Task DeleteAsync(Prescription prescription);
    }
}