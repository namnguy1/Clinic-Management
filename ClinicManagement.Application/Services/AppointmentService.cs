using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Application.Interfaces;
using ClinicManagement.Domain.Entities;

namespace ClinicManagement.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            await _appointmentRepository.AddAsync(appointment);
            return appointment;
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            await _appointmentRepository.DeleteAsync(id);
        }

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllAsync();
        }

        public async Task<Appointment> GetAppointmentAsync(int id)
        {
           return await _appointmentRepository.GetByIdAsync(id);
        }

        public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
        {
            var existingAppointment = await _appointmentRepository.GetByIdAsync(appointment.AppointmentId);
            if (existingAppointment == null)
            {
                return null;
            }

            // Cập nhật các trường thông tin
            existingAppointment.AppointmentDate = appointment.AppointmentDate;
            existingAppointment.Description = appointment.Description;
            existingAppointment.Status = appointment.Status;
            existingAppointment.UpdatedAt = appointment.UpdatedAt;

            await _appointmentRepository.UpdateAsync(existingAppointment);
            return existingAppointment;
        }
    }
}