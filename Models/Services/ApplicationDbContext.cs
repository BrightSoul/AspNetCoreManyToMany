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
        public DbSet<ProductTag> ProductTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>();
            modelBuilder.Entity<Tag>()
                        .HasMany(tag => tag.Products)
                        .WithMany(product => product.Tags)
                        .UsingEntity<ProductTag>(
                            tagProductBuilder => tagProductBuilder.HasOne(tagProduct => tagProduct.Product).WithMany().HasForeignKey(tagProduct => tagProduct.ProductId),
                            tagProductBuilder => tagProductBuilder.HasOne(tagProduct => tagProduct.Tag).WithMany().HasForeignKey(tagProduct => tagProduct.TagId),
                            tagProductBuilder => tagProductBuilder.ToTable("ProductTags")
                        );
        }
    }
}
