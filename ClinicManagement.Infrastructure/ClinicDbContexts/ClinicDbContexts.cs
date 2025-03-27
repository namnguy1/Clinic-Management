using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagement.Infrastructure.ClinicDbContexts
{
    public class ClinicDbContext : DbContext
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<TelemedicineSession> TelemedicineSessions { get; set; }
        public DbSet<NotificationLog> NotificationLogs { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<ExternalCalendar> ExternalCalendars { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Specialty> Specialty { get; set; }

        public DbSet<SpecialtyDoctor> SpecialtyDoctor { get; set; }

        public DbSet<SpecialtyHospitalOrClinic> SpecialtyHospitalOrClinic { get; set; }

        public DbSet<DoctorHospitalOrClinic> DoctorHospitalOrClinic { get; set; }

        public DbSet<HospitalOrClinic> HospitalOrClinic { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>(); // Lưu enum dưới dạng string
            // User - Doctor (1-1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Doctor)
                .WithOne(d => d.User)
                .HasForeignKey<Doctor>(d => d.UserId);

            // User - Patient (1-1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Patient)
                .WithOne(p => p.User)
                .HasForeignKey<Patient>(p => p.UserId);

            // Doctor - Appointment (1-N)
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Appointments)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Patient - Appointment (1-N)
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // MedicalRecord - Patient (1-N)
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Patient)
                .WithMany(p => p.MedicalRecords)
                .HasForeignKey(m => m.PatientId)
                .OnDelete(DeleteBehavior.NoAction); // Tránh vòng lặp

            // MedicalRecord - Doctor (1-N)
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Doctor)
                .WithMany(d => d.MedicalRecords)
                .HasForeignKey(m => m.DoctorId)
                .OnDelete(DeleteBehavior.NoAction); // Tránh vòng lặp

            // TelemedicineSession
            modelBuilder.Entity<TelemedicineSession>()
                .HasOne(t => t.Doctor)
                .WithMany()
                .HasForeignKey(t => t.DoctorId);
            // TelemedicineSession
            modelBuilder.Entity<TelemedicineSession>()
                .HasOne(t => t.Patient)
                .WithMany()
                .HasForeignKey(t => t.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            


            // Specialty - Doctor (N-N)
            modelBuilder.Entity<SpecialtyDoctor>()
                .HasKey(sd => new { sd.SpecialtyId, sd.DoctorId });

            modelBuilder.Entity<SpecialtyDoctor>()
                .HasOne(sd => sd.Specialty)
                .WithMany(s => s.SpecialtyDoctors)
                .HasForeignKey(sd => sd.SpecialtyId);

            modelBuilder.Entity<SpecialtyDoctor>()
                .HasOne(sd => sd.Doctor)
                .WithMany(d => d.SpecialtyDoctors)
                .HasForeignKey(sd => sd.DoctorId);

            // Specialty - HospitalOrClinic (N-N)
            modelBuilder.Entity<SpecialtyHospitalOrClinic>()
                .HasKey(sh => new { sh.SpecialtyId, sh.HospitalOrClinicId });

            modelBuilder.Entity<SpecialtyHospitalOrClinic>()
                .HasOne(sh => sh.Specialty)
                .WithMany(s => s.SpecialtyHospitalOrClinics)
                .HasForeignKey(sh => sh.SpecialtyId);

            modelBuilder.Entity<SpecialtyHospitalOrClinic>()
                .HasOne(sh => sh.HospitalOrClinic)
                .WithMany(h => h.SpecialtyHospitalOrClinics)
                .HasForeignKey(sh => sh.HospitalOrClinicId);

            // Doctor - HospitalOrClinic (N-N)
            modelBuilder.Entity<DoctorHospitalOrClinic>()
                .HasKey(dh => new { dh.DoctorId, dh.HospitalOrClinicId });

            modelBuilder.Entity<DoctorHospitalOrClinic>()
                .HasOne(dh => dh.Doctor)
                .WithMany(d => d.DoctorHospitalOrClinics)
                .HasForeignKey(dh => dh.DoctorId);

            modelBuilder.Entity<DoctorHospitalOrClinic>()
                .HasOne(dh => dh.HospitalOrClinic)
                .WithMany(h => h.DoctorHospitalOrClinics)
                .HasForeignKey(dh => dh.HospitalOrClinicId);

            base.OnModelCreating(modelBuilder);
        }
    }
}