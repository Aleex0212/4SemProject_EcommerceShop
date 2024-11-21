using OrderService.Domain.Models;

namespace OrderService.Application.Interfaces
{
  public interface ICommandService
  {
    /// <summary>
    /// Method creates an order.
    /// </summary>
    /// <param name="order">The order to be created</param>
    void CreateOrderAsync(Order order);

    /// <summary>
    /// Method updates an order.
    /// </summary>
    /// <param name="order"></param>
    void UpdateOrder(Order order);

    /// <summary>
    /// Method deletes an order.
    /// </summary>
    /// <param name="order"></param>
    void DeleteOrder(Order order);
  }
}
