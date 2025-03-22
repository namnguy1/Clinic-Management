using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Application.Dtos.MedicalRecord;
using ClinicManagement.Application.Interfaces;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Application.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }
        public async Task<MedicalRecord> CreateMedicalRecordAsync(MedicalRecordCreateDTO record)
        {
            var medicalRecord = new MedicalRecord
            {
                // Map properties from MedicalRecordCreateDTO to MedicalRecord
                PatientId = record.PatientId,
                DoctorId = record.DoctorId,
                Diagnosis = record.Diagnosis,
                TreatmentPlan = record.TreatmentPlan,
                // Note = record.Note,
                CreatedAt = DateTime.UtcNow
            };

            await _medicalRecordRepository.AddAsync(medicalRecord);
            return medicalRecord;
        }

        public async Task DeleteMedicalRecordAsync(int recordId)
        {
            var record = await _medicalRecordRepository.GetByIdAsync(recordId);
            if (record == null)
            {
                throw new Exception("Medical record not found.");
            }
            await _medicalRecordRepository.DeleteAsync(record);
        }

        public async Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordsAsync()
        {
            return await _medicalRecordRepository.GetAllAsync();
        }

        public async Task<MedicalRecord> GetMedicalRecordByIdAsync(int recordId)
        {
            var record = await _medicalRecordRepository.GetByIdAsync(recordId);
            if (record == null)
            {
                throw new Exception("Medical record not found.");
            }
            return record;
        }

        public async Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByDoctorAsync(int doctorId)
        {
            return await _medicalRecordRepository.GetByDoctorIdAsync(doctorId);
        }

        public async Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByPatientAsync(int patientId)
        {
            return await _medicalRecordRepository.GetByPatientIdAsync(patientId);
        }

        public async Task<MedicalRecord> UpdateMedicalRecordAsync(MedicalRecordUpdateDTO record)
        {
            var existingRecord = await _medicalRecordRepository.GetByIdAsync(record.RecordId);
            if (existingRecord == null)
            {
                throw new Exception("Medical record not found.");
            }

            // Cập nhật các trường cần thiết
            // existingRecord.Diagnosis = record.Diagnosis;
            // existingRecord.TreatmentPlan = record.TreatmentPlan;
            // existingRecord.Note = record.Note;
            // Bạn có thể cập nhật thêm các trường khác nếu cần, ví dụ thêm danh sách prescription

            var updateDto = new MedicalRecord
            {
                RecordId = record.RecordId,
                Diagnosis = record.Diagnosis,
                TreatmentPlan = record.TreatmentPlan,
            };

            await _medicalRecordRepository.UpdateAsync(updateDto);
            return existingRecord;
        }
    }
}