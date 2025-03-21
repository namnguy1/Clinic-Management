using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Application.Interfaces
{
    public interface IPatientRepository
    {
         Task<Patient> GetByIdAsync(int PatientId);
        Task<Patient> GetByEmailAsync(string email);
        Task<IEnumerable<Patient>> GetAllAsync();
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(Patient patient);
    }
}