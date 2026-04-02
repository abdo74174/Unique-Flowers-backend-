using Microsoft.EntityFrameworkCore;
using FlowerShop.API.Models;

namespace FlowerShop.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Enhancement: Query filter for soft delete
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
    }
}
