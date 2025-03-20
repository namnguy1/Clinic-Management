using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClinicManagement.Application.Interfaces;
using ClinicManagement.Domain.Entities;
using ClinicManagement.Application.Dtos.Appointment;
using ClinicManagement.Domain.Enums;

namespace ClinicManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// Lấy thông tin cuộc hẹn theo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var appointment = await _appointmentService.GetAppointmentAsync(id);
            if (appointment == null)
                return NotFound(new { Message = "Appointment not found" });

            return Ok(appointment);
        }

        /// <summary>
        /// Lấy danh sách tất cả cuộc hẹn
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            List<Appointment> appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        /// <summary>
        /// Tạo một cuộc hẹn mới
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequest request)
        {
            if (request == null)
                return BadRequest(new { Message = "Invalid appointment data" });
            var appointment = new Appointment
            {
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                AppointmentDate = request.AppointmentDate,
                Description = request.Description,
                Status = AppointmentStatus.Scheduled,    // ví dụ default
                CreatedAt = DateTime.UtcNow
            };
            var createdAppointment = await _appointmentService.CreateAppointmentAsync(appointment);
            return CreatedAtAction(nameof(GetAppointment), new { id = createdAppointment.AppointmentId }, createdAppointment);
        }

        /// <summary>
        /// Cập nhật thông tin cuộc hẹn
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentRequest request)
        {
            var existingAppointment = await _appointmentService.GetAppointmentAsync(id);
            if (existingAppointment == null) return NotFound();
            existingAppointment.AppointmentDate = request.AppointmentDate;
            existingAppointment.Description = request.Description;
            existingAppointment.Status = request.Status;
            existingAppointment.UpdatedAt = DateTime.UtcNow;

            var updated = await _appointmentService.UpdateAppointmentAsync(existingAppointment);
            return Ok(updated);
        }

        /// <summary>
        /// Xóa một cuộc hẹn theo ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var existingAppointment = await _appointmentService.GetAppointmentAsync(id);
            if (existingAppointment == null)
                return NotFound(new { Message = "Appointment not found" });

            await _appointmentService.DeleteAppointmentAsync(id);
            return NoContent();
        }

    }
}