using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostKeeper
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CostsDbContext>
    {
        public CostsDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CostsDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);

            return new CostsDbContext(optionsBuilder.Options);
        }
    }
}
