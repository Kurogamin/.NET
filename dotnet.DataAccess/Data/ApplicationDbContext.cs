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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Studio>().HasData(
            new Studio { Id = 1, Name = "Blizzard", DisplayOrder = 1 },
            new Studio { Id = 2, Name = "Bethesda", DisplayOrder = 2 },
            new Studio { Id = 3, Name = "CD Projekt Red", DisplayOrder = 3 }
            );

        modelBuilder.Entity<Game>().HasData(
            new Game { Description = "A game about a witcher", Genre = GameGenre.RPG, Id = 1, Title = "The Witcher 3", StudioId = 3 },
            new Game { Description = "A game about a dragonborn", Genre = GameGenre.RPG, Id = 2, Title = "The Elder Scrolls V: Skyrim", StudioId = 2 },
            new Game { Description = "A game about world of warcraft", Genre = GameGenre.MMORPG, Id = 3, Title = "World of Warcraft", StudioId = 1 }
            );
    }
}