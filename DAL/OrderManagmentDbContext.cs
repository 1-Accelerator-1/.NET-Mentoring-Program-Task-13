using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    internal class OrderManagmentDbContext : DbContext
    {
        public OrderManagmentDbContext()
        {
        }

        public OrderManagmentDbContext(DbContextOptions<OrderManagmentDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Order>().ToTable("Order");
            builder.Entity<Product>().ToTable("Product");
        }
    }
}
