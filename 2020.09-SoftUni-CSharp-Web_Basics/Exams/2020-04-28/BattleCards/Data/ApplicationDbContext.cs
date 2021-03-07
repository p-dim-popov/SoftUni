using BattleCards.Models;

namespace BattleCards.Data
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<UserCard> UserCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(ent =>
            {
                ent.HasKey(e => e.Id);
            });

            modelBuilder.Entity<UserCard>(ent =>
            {
                ent.HasKey(e => new {e.UserId, e.CardId});

                ent.HasOne(e => e.User)
                    .WithMany(u => u.UserCard)
                    .HasForeignKey(uc => uc.UserId);

                ent.HasOne(e => e.Card)
                    .WithMany(c => c.UserCard)
                    .HasForeignKey(uc => uc.CardId);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
