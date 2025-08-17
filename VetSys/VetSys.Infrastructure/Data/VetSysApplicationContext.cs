using Microsoft.EntityFrameworkCore;
using System;

using VetSys.Domain.Entities;

namespace VetSys.Infrastructure.Data
{
    public class VetSysApplicationContext: DbContext
    {
        public VetSysApplicationContext(DbContextOptions options) : base(options) { }


        public DbSet<Animal> Animals {  get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicineTreatment> MedicineTreatments { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Vet> Vets { get; set; }
    }
}
