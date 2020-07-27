using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    using Microsoft.EntityFrameworkCore;

    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PatientMedicament> PatientMedicaments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(
                        "Server=.;" +
                        "Database=HospitalDatabase;" +
                        "Integrated Security=True;"
                    );
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Visitation>()
                .HasOne<Patient>()
                .WithMany(p => p.Visitations)
                .HasForeignKey(v => v.PatientId);

            modelBuilder
                .Entity<Diagnose>()
                .HasOne<Patient>()
                .WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.PatientId);

            modelBuilder
                .Entity<PatientMedicament>()
                .HasKey(pm => new {pm.PatientId, pm.MedicamentId});
            
            modelBuilder
                .Entity<PatientMedicament>()
                .HasOne<Patient>()
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(pm => pm.PatientId);

            modelBuilder
                .Entity<PatientMedicament>()
                .HasOne<Medicament>()
                .WithMany(m => m.Prescriptions)
                .HasForeignKey(pm => pm.MedicamentId);

            modelBuilder
                .Entity<Visitation>()
                .HasOne<Doctor>()
                .WithMany(d => d.Visitations)
                .HasForeignKey(v => v.DoctorId);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}