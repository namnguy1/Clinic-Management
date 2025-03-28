using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Application.Dtos.MedicalRecord;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Application.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<MedicalRecord> CreateMedicalRecordAsync(MedicalRecordCreateDTO record);
        Task<MedicalRecord> GetMedicalRecordByIdAsync(int recordId);
        Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordsAsync();
        Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByPatientAsync(int patientId);
        Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByDoctorAsync(int doctorId);
        Task<MedicalRecord> UpdateMedicalRecordAsync(MedicalRecordUpdateDTO record);
        Task DeleteMedicalRecordAsync(int recordId);
    }
}