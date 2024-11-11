using OrderService.Domain.Models;

namespace OrderService.Application.Interfaces
{
  public interface ICommandService
  {
    /// <summary>
    /// Create an order.
    /// </summary>
    /// <param name="order">
    Task CreateOrderAsync(Order order);

    /// <summary>
    /// Delete an order.
    /// </summary>
    /// <param name="orderId">
    Task DeleteOrderAsync(Guid orderId);
  }
}
