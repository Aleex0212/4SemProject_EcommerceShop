using OrderService.Domain.Models;

namespace OrderService.Application.Interfaces
{
  public interface ICommandService
  {
    /// <summary>
    /// Create an order.
    /// </summary>
    /// <param name="order">The order to be created</param>
    void CreateOrderAsync(Order order);

    void UpdateOrder(Order order);

    void DeleteOrder(Order order);
  }
}
