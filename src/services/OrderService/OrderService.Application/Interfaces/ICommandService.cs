using OrderService.Domain.Models;

namespace OrderService.Application.Interfaces
{
  public interface ICommandService
  {
    /// <summary>
    /// Create an order.
    /// </summary>
    /// <param name="order">
    Task CreateOrder(Order order);

    /// <summary>
    /// Delete an order.
    /// </summary>
    /// <param name="orderId">
    Task DeleteOrder(Guid orderId);
  }
}
