using CostKeeper.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CostKeeper
{
    public class CostsDbContext(DbContextOptions<CostsDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Check> Checks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
    }
}
