using OrderService.Domain.Models;

namespace OrderService.Application.Services
{
  public interface IOrderRepository
  {
    Task AddOrderAsync(Order order);

    void UpdateOrder(Order order);

    void DeleteOrder(Order order);
  }
}
