using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Application.Dtos.Prescription;

namespace ClinicManagement.Application.Interfaces
{
    public interface IPrescriptionService
    {
        Task<PrescriptionResponseDto> CreatePrescriptionAsync(PrescriptionCreateDto dto);
        Task<PrescriptionResponseDto> UpdatePrescriptionAsync(PrescriptionUpdateDto dto);
        Task<PrescriptionResponseDto> GetPrescriptionByIdAsync(int id);
        Task<IEnumerable<PrescriptionResponseDto>> GetAllPrescriptionsAsync();
        Task<IEnumerable<PrescriptionResponseDto>> GetPrescriptionsByMedicalRecordAsync(int recordId);
        Task DeletePrescriptionAsync(int id);
    }
}