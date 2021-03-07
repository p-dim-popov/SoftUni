namespace P03_FootballBetting.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        {
        }

        public FootballBettingContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(
                    "Server=.;Database=FootballBetting;Integrated Security=True;"
                        );
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Team>()
                .HasOne(t => t.PrimaryKitColor)
                .WithMany(c => c.PrimaryKitTeams)
                .HasForeignKey(c => c.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Team>()
                .HasOne(t => t.SecondaryKitColor)
                .WithMany(c => c.SecondaryKitTeams)
                .HasForeignKey(t => t.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Team>()
                .HasOne<Town>()
                .WithMany(town => town.Teams)
                .HasForeignKey(team => team.TownId);

            modelBuilder
                .Entity<Game>()
                .HasOne(g => g.HomeTeam)
                .WithMany(ht => ht.HomeGames)
                .HasForeignKey(hg => hg.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Game>()
                .HasOne(g => g.AwayTeam)
                .WithMany(at => at.AwayGames)
                .HasForeignKey(ag => ag.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Town>()
                .HasOne<Country>()
                .WithMany(c => c.Towns)
                .HasForeignKey(t => t.CountryId);

            modelBuilder
                .Entity<Player>()
                .HasOne<Team>()
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);

            modelBuilder
                .Entity<Player>()
                .HasOne<Position>()
                .WithMany(p => p.Players)
                .HasForeignKey(p => p.PositionId);

            modelBuilder
                .Entity<PlayerStatistic>()
                .HasKey(ps => new {ps.PlayerId, ps.GameId});

            modelBuilder
                .Entity<PlayerStatistic>()
                .HasOne<Game>()
                .WithMany(g => g.PlayerStatistics)
                .HasForeignKey(ps => ps.GameId);

            modelBuilder
                .Entity<PlayerStatistic>()
                .HasOne<Player>()
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(ps => ps.PlayerId);

            modelBuilder
                .Entity<Bet>()
                .HasOne<Game>()
                .WithMany(g => g.Bets)
                .HasForeignKey(b => b.GameId);

            modelBuilder
                .Entity<Bet>()
                .HasOne<User>()
                .WithMany(u => u.Bets)
                .HasForeignKey(b => b.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
