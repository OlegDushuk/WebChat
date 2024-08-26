using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WebChat.DAL;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
  public AppDbContext CreateDbContext(string[] args)
  {
    var configuration = new ConfigurationBuilder()
      .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WebChat.API"))
      .AddJsonFile("appsettings.json")
      .Build();
    
    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    
    var connectionString = configuration.GetConnectionString("Db");
    optionsBuilder.UseSqlServer(connectionString);

    return new AppDbContext(optionsBuilder.Options);
  }
}