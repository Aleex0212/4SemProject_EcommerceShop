using OrderService.Application.Interfaces;
using OrderService.Domain.Models;
using OrderService.Persistence.Db;

namespace OrderService.Persistence.Repositories
{
  public class CommandRepository : ICommandRepository
  {
    private readonly OrderData _orders;
    public CommandRepository(OrderData orders)
    {
      _orders = orders;
    }

    public void AddOrderAsync(Order order)
    {
      _orders.Orders.Add(order);
    }

    public void UpdateOrder(Order order)
    {
      var existingOrder = _orders.Orders.FirstOrDefault(o => o.Id == order.Id);
      _orders.Orders.Remove(existingOrder);
      _orders.Orders.Add(order);
    }

    public void DeleteOrder(Order order)
    {
      var existingOrder = _orders.Orders.FirstOrDefault(o => o.Id == order.Id);
      _orders.Orders.Remove(existingOrder);
    }
  }
}
