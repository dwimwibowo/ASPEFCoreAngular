using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DutchTreat.Data.Entities;

/* Entity Framework Scripts
 * 
 * dotnet-ef migrations add <title>
 * dotnet-ef migrations remove <title>
 * dotnet-ef database update
 * 
 */

namespace DutchTreat.Data
{
    public class DutchContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        // Constructor
        public DutchContext(DbContextOptions<DutchContext> options): base(options)
        {

        }

        // Seeding
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasData(new Order()
                {
                    Id = 1,
                    OrderDate = DateTime.UtcNow,
                    OrderNumber = "12345"
                });
        }
    }
}
