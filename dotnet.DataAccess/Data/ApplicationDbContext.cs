using Microsoft.EntityFrameworkCore;
using dotnet.Models;

namespace dotnet.DataAccess.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}

	public DbSet<Game> Games { get; set; }
	public DbSet<Studio> Studios { get; set; }
	public DbSet<Genre> Genres { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Studio>().HasData(
			new Studio { Id = 1, Name = "Blizzard", DisplayOrder = 1 },
			new Studio { Id = 2, Name = "Bethesda", DisplayOrder = 2 },
			new Studio { Id = 3, Name = "CD Projekt Red", DisplayOrder = 3 }
			);

		modelBuilder.Entity<Genre>().HasData(
			new Genre { Id = 1, Name = "MMORPG" },
			new Genre { Id = 2, Name = "RPG" },
			new Genre { Id = 3, Name = "Action RPG" }
			);

		modelBuilder.Entity<Game>().HasData(
			new Game { Description = "A game about a witcher", GenreId = 2, Id = 1, Title = "The Witcher 3", StudioId = 3 },
			new Game { Description = "A game about a dragonborn", GenreId = 2, Id = 2, Title = "The Elder Scrolls V: Skyrim", StudioId = 2 },
			new Game { Description = "A game about world of warcraft", GenreId = 1, Id = 3, Title = "World of Warcraft", StudioId = 1 }
			);
	}
}