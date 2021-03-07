using SharedTrip.Data.Models;

namespace SharedTrip.Data
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public DbSet<UserTrip> UserTrips { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }

        public ApplicationDbContext(DbContextOptions db)
            : base(db)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;" +
                                            "Database=SharedTrip;" +
                                            "Integrated Security=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>(ent =>
            {
                ent.HasKey(e => new {e.UserId, e.TripId});

                ent.HasOne(e => e.Trip)
                    .WithMany(t => t.UserTrips)
                    .HasForeignKey(e => e.TripId);

                ent.HasOne(e => e.User)
                    .WithMany(u => u.UserTrips)
                    .HasForeignKey(e => e.UserId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
