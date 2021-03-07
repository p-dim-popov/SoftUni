namespace SULS.Data
{
    using Models;
    using Microsoft.EntityFrameworkCore;

    public class SULSContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Submission> Submissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(ent =>
            {
                ent.HasKey(u => u.Id);

                ent.Property(u => u.Username)
                    .HasMaxLength(20)
                    .IsRequired();

                ent.Property(u => u.Email)
                    .IsRequired();

                ent.Property(u => u.Password)
                    .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=.;" +
                                  "Database=Suls;" +
                                  "Integrated Security=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}