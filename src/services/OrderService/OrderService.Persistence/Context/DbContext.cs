using Microsoft.EntityFrameworkCore;
using OrderService.Domain;
using OrderService.Domain.Models;

namespace OrderService.Persistence.Context
{
  public class DbContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public DbContext(DbContextOptions<DbContext> options) : base(options)
    {

    }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<ProductLine> ProductLines { get; set; }
    public DbSet<Product> Products { get; set; }
  }
}
