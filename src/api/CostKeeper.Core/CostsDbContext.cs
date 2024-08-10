using CostKeeper.Models;
using Microsoft.EntityFrameworkCore;

namespace CostKeeper
{
    public class CostsDbContext(DbContextOptions<CostsDbContext> options) : DbContext(options)
    {
		public DbSet<Product> Products { get; set; }
        public DbSet<Check> Checks { get; set; }
    }
}
