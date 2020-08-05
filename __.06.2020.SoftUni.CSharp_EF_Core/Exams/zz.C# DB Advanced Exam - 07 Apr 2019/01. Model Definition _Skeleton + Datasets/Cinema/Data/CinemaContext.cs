using Cinema.Data.Models;

namespace Cinema.Data
{
    using Microsoft.EntityFrameworkCore;

    public class CinemaContext : DbContext
    {
        public CinemaContext()  { }

        public CinemaContext(DbContextOptions options)
            : base(options)   { }

        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Projection> Projections { get; set; }
        public DbSet<Movie> Movies { get; set; }
        
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
                .Entity<Ticket>(e =>
            {
                e
                    .HasOne(t => t.Customer)
                    .WithMany(c => c.Tickets)
                    .HasForeignKey(t => t.CustomerId);
            
                e
                    .HasOne(t => t.Projection)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(t => t.ProjectionId);
            });
            
            modelBuilder
                .Entity<Projection>(e =>
                {
                    e
                        .HasOne(p => p.Hall)
                        .WithMany(h => h.Projections)
                        .HasForeignKey(p => p.HallId);
            
                    e
                        .HasOne(p => p.Movie)
                        .WithMany(m => m.Projections)
                        .HasForeignKey(p => p.MovieId);
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}