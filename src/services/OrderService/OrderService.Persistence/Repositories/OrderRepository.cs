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
      await _dbContext.Orders.AddAsync(order);
    }

    public void UpdateOrder(Order order)
    {
      _dbContext.Update(order);
    }

    public void DeleteOrder(Order order)
    {
      _dbContext.Remove(order);
    }
  }
}
