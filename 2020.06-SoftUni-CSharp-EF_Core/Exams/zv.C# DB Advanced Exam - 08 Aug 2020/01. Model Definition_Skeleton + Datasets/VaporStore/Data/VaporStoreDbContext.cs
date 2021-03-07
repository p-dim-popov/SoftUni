﻿using VaporStore.Data.Models;

namespace VaporStore.Data
{
	using Microsoft.EntityFrameworkCore;

	public class VaporStoreDbContext : DbContext
	{
		public VaporStoreDbContext()
		{
		}

		public VaporStoreDbContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<Game> Games { get; set; }
		public DbSet<Developer> Developers { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<GameTag> GameTags { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Card> Cards { get; set; }
		public DbSet<Purchase> Purchases { get; set; }
		
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			if (!options.IsConfigured)
			{
				options
					.UseSqlServer(Configuration.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder model)
		{
			model
				.Entity<GameTag>(ent =>
				{
					ent
						.HasKey(e => new {e.GameId, e.TagId});

					ent
						.HasOne(e => e.Game)
						.WithMany(g => g.GameTags)
						.HasForeignKey(gt => gt.GameId);

					ent
						.HasOne(e => e.Tag)
						.WithMany(t => t.GameTags)
						.HasForeignKey(gt => gt.TagId);
				});
		}
	}
}