using Microsoft.EntityFrameworkCore;
using ASP.NET_Core_MVC_Application.Models;

namespace ASP.NET_Core_MVC_Application.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Game> Games { get; set; }
}
