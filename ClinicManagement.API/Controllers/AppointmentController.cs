using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClinicManagement.Application.Interfaces;
using ClinicManagement.Domain.Entities;

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
        public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
        {
            if (appointment == null)
                return BadRequest(new { Message = "Invalid appointment data" });

            var createdAppointment = await _appointmentService.CreateAppointmentAsync(appointment);
            return CreatedAtAction(nameof(GetAppointment), new { id = createdAppointment.AppointmentId }, createdAppointment);
        }

        /// <summary>
        /// Cập nhật thông tin cuộc hẹn
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
                return BadRequest(new { Message = "Appointment ID mismatch" });

            var updatedAppointment = await _appointmentService.UpdateAppointmentAsync(appointment);
            if (updatedAppointment == null)
                return NotFound(new { Message = "Appointment not found" });

            return Ok(updatedAppointment);
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