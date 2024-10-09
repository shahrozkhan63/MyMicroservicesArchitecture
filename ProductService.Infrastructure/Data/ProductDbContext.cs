using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the Product entity
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId); // Primary key for Product

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired() // Make Name required
                .HasMaxLength(100); // Set maximum length for Name

            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(500); // Set maximum length for Description

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2); // Set precision and scale for Price
        }
    }
}
