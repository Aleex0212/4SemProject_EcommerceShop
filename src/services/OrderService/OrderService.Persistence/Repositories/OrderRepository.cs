using EcommerceShop.Common.Enum;
using OrderService.Application.Services;
using OrderService.Domain.Models;
using OrderService.Persistence.Db;

namespace OrderService.Persistence.Repositories
{
  public class OrderRepository : IOrderRepository
  {
    private readonly OrderData Orders;
    public OrderRepository(OrderData orders)
    {
      Orders = orders;
    }

    public async Task AddOrderAsync(Order order)
    {
      Orders.Orders.Add(order);
    }

    public void UpdateOrder(Order order)
    {
      var existingOrder = Orders.Orders.FirstOrDefault(o => o.Id == order.Id);
      Orders.Orders.Remove(existingOrder);
      Orders.Orders.Add(order);
    }

    public void DeleteOrder(Order order)
    {
      var existingOrder = Orders.Orders.FirstOrDefault(o => o.Id == order.Id);
      Orders.Orders.Remove(existingOrder);
    }
  }
}
