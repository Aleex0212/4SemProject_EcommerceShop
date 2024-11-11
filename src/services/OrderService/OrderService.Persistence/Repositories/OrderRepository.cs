using OrderService.Application.Services;
using OrderService.Domain.Models;
using OrderContext = OrderService.Persistence.Context.OrderContext;

namespace OrderService.Persistence.Repositories
{
  public class OrderRepository : IOrderRepository
  {
    private readonly OrderContext _dbContext;

    public OrderRepository(OrderContext dbContext)
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
