using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Application.Dtos.Prescription;
using ClinicManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {
         private readonly IPrescriptionService _service;

        public PrescriptionController(IPrescriptionService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            var list = await _service.GetAllPrescriptionsAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetPrescription(int id)
        {
            try
            {
                var prescription = await _service.GetPrescriptionByIdAsync(id);
                return Ok(prescription);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet("medicalrecord/{recordId}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetPrescriptionsByMedicalRecord(int recordId)
        {
            var list = await _service.GetPrescriptionsByMedicalRecordAsync(recordId);
            return Ok(list);
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> CreatePrescription([FromBody] PrescriptionCreateDto dto)
        {
            if (dto == null)
                return BadRequest(new { Message = "Invalid data." });

            var created = await _service.CreatePrescriptionAsync(dto);
            return CreatedAtAction(nameof(GetPrescription), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdatePrescription(int id, [FromBody] PrescriptionUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { Message = "ID mismatch." });

            try
            {
                var updated = await _service.UpdatePrescriptionAsync(dto);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            try
            {
                await _service.DeletePrescriptionAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}