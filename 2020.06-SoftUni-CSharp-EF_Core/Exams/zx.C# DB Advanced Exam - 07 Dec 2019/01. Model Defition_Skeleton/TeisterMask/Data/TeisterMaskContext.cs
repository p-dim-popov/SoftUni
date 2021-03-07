using TeisterMask.Data.Models;

namespace TeisterMask.Data
{
    using Microsoft.EntityFrameworkCore;

    public class TeisterMaskContext : DbContext
    {
        public TeisterMaskContext() { }

        public TeisterMaskContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<EmployeeTask> EmployeesTasks { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<EmployeeTask>(employeesTasks =>
                {
                    employeesTasks
                        .HasKey(x => new {x.EmployeeId, x.TaskId});

                    employeesTasks
                        .HasOne(et => et.Employee)
                        .WithMany(e => e.EmployeesTasks)
                        .HasForeignKey(et => et.EmployeeId);

                    employeesTasks
                        .HasOne(et => et.Task)
                        .WithMany(t => t.EmployeesTasks)
                        .HasForeignKey(et => et.TaskId);
                });
        }
    }
}