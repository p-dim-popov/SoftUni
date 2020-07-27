using Microsoft.EntityFrameworkCore;

namespace P03_SalesDatabase.Data
{
    using Models;
    
    public class SalesContext : DbContext
    {
        public SalesContext()
        {
        }

        public SalesContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Sale> Sales { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(
                        "Server=.;" +
                        "Database=SalesDatabase;" +
                        "Integrated Security=True;"
                    );
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Sale>()
                .HasOne<Product>()
                .WithMany(p => p.Sales)
                .HasForeignKey(s => s.ProductId);

            modelBuilder
                .Entity<Sale>()
                .HasOne<Customer>()
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId);

            modelBuilder
                .Entity<Sale>()
                .HasOne<Store>()
                .WithMany(s => s.Sales)
                .HasForeignKey(s => s.StoreId);

            base.OnModelCreating(modelBuilder);
        }
    }
}