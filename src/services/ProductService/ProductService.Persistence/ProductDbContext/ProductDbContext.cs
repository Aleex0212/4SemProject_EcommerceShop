using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Models;

namespace ProductService.Persistence.ProductDbContext
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}