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
            //// Does not work with current project version
            // modelBuilder
            //     .Entity<AuthorBook>()
            //     .HasKey(ab => new {ab.AuthorId, ab.BookId});
            //
            // modelBuilder
            //     .Entity<AuthorBook>()
            //     .HasOne<Author>()
            //     .WithMany(a => a.AuthorsBooks)
            //     .HasForeignKey(ab => ab.AuthorId);
            //
            // modelBuilder
            //     .Entity<AuthorBook>()
            //     .HasOne<Book>()
            //     .WithMany(b => b.AuthorsBooks)
            //     .HasForeignKey(ab => ab.BookId);
            
            modelBuilder.Entity<AuthorBook>(entity =>
            {
                entity.HasKey(ab => new { ab.AuthorId, ab.BookId });

                entity
                    .HasOne(ab => ab.Author)
                    .WithMany(a => a.AuthorsBooks)
                    .HasForeignKey(ab => ab.AuthorId);

                entity
                    .HasOne(ab => ab.Book)
                    .WithMany(b => b.AuthorsBooks)
                    .HasForeignKey(ab => ab.BookId);
            });
        }
    }
}