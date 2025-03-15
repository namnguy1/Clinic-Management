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
        // public DbSet<Appointment> Appointments { get; set; }
        // public DbSet<MedicalRecord> MedicalRecords { get; set; }
        // public DbSet<Prescription> Prescriptions { get; set; }
        // Thêm DbSet cho các entity khác nếu cần.

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        // }   
    }
}