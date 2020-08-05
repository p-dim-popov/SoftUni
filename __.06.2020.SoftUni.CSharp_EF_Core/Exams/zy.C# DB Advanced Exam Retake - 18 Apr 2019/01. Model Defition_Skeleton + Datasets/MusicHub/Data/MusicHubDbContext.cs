using MusicHub.Data.Models;

namespace MusicHub.Data
{
    using Microsoft.EntityFrameworkCore;

    public class MusicHubDbContext : DbContext
    {
        public MusicHubDbContext()
        {
        }

        public MusicHubDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<SongPerformer> SongsPerformers { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<SongPerformer>(ent =>
                {
                    ent
                        .HasKey(sp 
                            => new {sp.PerformerId, sp.SongId});

                    ent
                        .HasOne(sp => sp.Performer)
                        .WithMany(p => p.PerformerSongs)
                        .HasForeignKey(sp => sp.PerformerId);

                    ent
                        .HasOne(sp => sp.Song)
                        .WithMany(s => s.SongPerformers)
                        .HasForeignKey(sp => sp.SongId);
                });
            
            
            base.OnModelCreating(builder);
        }
    }
}
