using OrderService.Application.Services;
using OrderService.Domain.Models;
using DbContext = OrderService.Persistence.Context.DbContext;

namespace OrderService.Persistence.Repositories
{
  public class OrderRepository : IOrderRepository
  {
    private readonly DbContext _dbContext;

    public OrderRepository(DbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task AddOrderAsync(Order order)
    {
      _dbContext.Orders.Add(order);
      await _dbContext.SaveChangesAsync();
    }
  }
}
