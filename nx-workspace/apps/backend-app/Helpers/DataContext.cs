namespace BackendApp.Helpers;

using Microsoft.EntityFrameworkCore;
using BackendApp.Entities;

public class DataContext : DbContext
{
  protected readonly IConfiguration Configuration;

  public DataContext(IConfiguration configuration)
  {
    Configuration = configuration;
  }

  protected override void OnConfiguring(DbContextOptionsBuilder options)
  {
    // connect to sql server with connection string from app settings
    options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
  }

  public DbSet<Users> Users { get; set; }
}