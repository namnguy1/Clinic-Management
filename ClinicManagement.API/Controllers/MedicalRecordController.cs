using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Application.Dtos.MedicalRecord;
using ClinicManagement.Application.Interfaces;
using ClinicManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        /// <summary>
        /// Lấy danh sách tất cả bệnh án
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Doctor")] // Ví dụ, chỉ bác sĩ và admin có quyền xem toàn bộ
        public async Task<IActionResult> GetAllMedicalRecords()
        {
            var records = await _medicalRecordService.GetAllMedicalRecordsAsync();
            return Ok(records);
        }

        /// <summary>
        /// Lấy chi tiết một bệnh án theo ID
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Doctor,Patient")]
        public async Task<IActionResult> GetMedicalRecord(int id)
        {
            try
            {
                var record = await _medicalRecordService.GetMedicalRecordByIdAsync(id);
                return Ok(record);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Tạo mới bệnh án
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> CreateMedicalRecord([FromBody] MedicalRecordCreateDTO recordDto)
        {
            if (recordDto == null)
                return BadRequest(new { Message = "Invalid record data." });

            var createdRecord = await _medicalRecordService.CreateMedicalRecordAsync(recordDto);
            return CreatedAtAction(nameof(GetMedicalRecord), new { id = createdRecord.RecordId }, createdRecord);
        }

        /// <summary>
        /// Cập nhật bệnh án
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateMedicalRecord(int id, [FromBody] MedicalRecordUpdateDTO recordDto)
        {
            if (id != recordDto.RecordId)
                return BadRequest(new { Message = "ID mismatch." });

            try
            {
                var updatedRecord = await _medicalRecordService.UpdateMedicalRecordAsync(recordDto);
                return Ok(updatedRecord);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Xoá bệnh án
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Doctor")] // Chỉ admin có thể xoá
        public async Task<IActionResult> DeleteMedicalRecord(int id)
        {
            try
            {
                await _medicalRecordService.DeleteMedicalRecordAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy bệnh án theo bệnh nhân
        /// </summary>
        [HttpGet("patient/{patientId}")]
        [Authorize(Roles = "Patient,Doctor,Admin")]
        public async Task<IActionResult> GetMedicalRecordsByPatient(int patientId)
        {
            var records = await _medicalRecordService.GetMedicalRecordsByPatientAsync(patientId);
            return Ok(records);
        }

        /// <summary>
        /// Lấy bệnh án theo bác sĩ
        /// </summary>
        [HttpGet("doctor/{doctorId}")]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> GetMedicalRecordsByDoctor(int doctorId)
        {
            var records = await _medicalRecordService.GetMedicalRecordsByDoctorAsync(doctorId);
            return Ok(records);
        }
    }
}