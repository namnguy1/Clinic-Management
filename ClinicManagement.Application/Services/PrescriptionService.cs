using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Application.Dtos.Prescription;
using ClinicManagement.Application.Interfaces;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Application.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _repository;

        public PrescriptionService(IPrescriptionRepository repository)
        {
            _repository = repository;
        }
        public async Task<PrescriptionResponseDto> CreatePrescriptionAsync(PrescriptionCreateDto dto)
        {
            // Tự map (không dùng AutoMapper)
            var prescription = new Prescription
            {
                MedicalRecordId = dto.MedicalRecordId,
                MedicineName = dto.MedicineName ?? throw new ArgumentNullException(nameof(dto.MedicineName), "MedicineName cannot be null"),
                Dosage = dto.Dosage ?? throw new ArgumentNullException(nameof(dto.Dosage), "Dosage cannot be null"),
                Instructions = dto.Instructions
            };

            var created = await _repository.AddAsync(prescription);
            return MapToResponse(created);
        }

        public async Task<PrescriptionResponseDto> UpdatePrescriptionAsync(PrescriptionUpdateDto dto)
        {
            // Tìm prescription hiện có
            var existing = await _repository.GetByIdAsync(dto.Id);
            if (existing == null)
                throw new Exception("Prescription not found.");

            // Cập nhật thủ công
            existing.MedicineName = dto.MedicineName;
            existing.Dosage = dto.Dosage;
            existing.Instructions = dto.Instructions;

            var updated = await _repository.UpdateAsync(existing);
            return MapToResponse(updated);
        }

        public async Task<PrescriptionResponseDto> GetPrescriptionByIdAsync(int id)
        {
            var prescription = await _repository.GetByIdAsync(id);
            if (prescription == null)
                throw new Exception("Prescription not found.");
            return MapToResponse(prescription);
        }

        public async Task<IEnumerable<PrescriptionResponseDto>> GetAllPrescriptionsAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(p => MapToResponse(p));
        }

        public async Task<IEnumerable<PrescriptionResponseDto>> GetPrescriptionsByMedicalRecordAsync(int recordId)
        {
            var list = await _repository.GetByMedicalRecordIdAsync(recordId);
            return list.Select(p => MapToResponse(p));
        }

        public async Task DeletePrescriptionAsync(int id)
        {
            var prescription = await _repository.GetByIdAsync(id);
            if (prescription == null)
                throw new Exception("Prescription not found.");
            
            await _repository.DeleteAsync(prescription);
        }

        // Hàm map thủ công (thay cho AutoMapper)
        private PrescriptionResponseDto MapToResponse(Prescription p)
        {
            return new PrescriptionResponseDto
            {
                Id = p.PrescriptionId,
                MedicalRecordId = p.MedicalRecordId,
                MedicineName = p.MedicineName,
                Dosage = p.Dosage,
                Instructions = p.Instructions
            };
        }
    }
}