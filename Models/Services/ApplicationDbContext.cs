using AspNetCoreManyToManyDemo.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreManyToManyDemo.Models.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>();
            modelBuilder.Entity<Tag>();
        }
    }
}
