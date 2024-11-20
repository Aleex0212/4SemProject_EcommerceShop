using OrderService.Domain.Models;

namespace OrderService.Application.Interfaces
{
  public interface ICommandRepository
  {
    void AddOrderAsync(Order order);

    void UpdateOrder(Order order);

    void DeleteOrder(Order order);
  }
}
