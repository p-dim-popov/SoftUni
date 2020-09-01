using BookShop.Data.Models;

namespace BookShop.Data
{
    using Microsoft.EntityFrameworkCore;

    public class BookShopContext : DbContext
    {
        public BookShopContext() { }

        public BookShopContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AuthorBook> AuthorsBooks { get; set; }
        
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
            modelBuilder.Entity<AuthorBook>(ent =>
            {
                ent
                    .HasKey(e => new {e.AuthorId, e.BookId});

                ent
                    .HasOne(e => e.Author)
                    .WithMany(a => a.AuthorsBooks)
                    .HasForeignKey(ab => ab.AuthorId);

                ent
                    .HasOne(e => e.Book)
                    .WithMany(b => b.AuthorsBooks)
                    .HasForeignKey(ab => ab.BookId);
            });
        }
    }
}