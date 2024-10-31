using OrderService.Domain.Models;

namespace OrderService.Application.Interfaces
{
  public interface IQueryService
  {
    /// <summary>
    /// Retrieves an order by its ID.
    /// </summary>
    /// <param name="orderId">The unique identifier of the order.</param>
    /// <param name="customerId">The unique identifier of the customer.</param>
    /// <returns>The task result contains an order.</returns>
    Task<Order> GetOrder(Guid orderId, Guid customerId);

    /// <summary>
    /// Retrieves a collection of all orders.
    /// </summary>
    /// <returns>The task Retrieves a collection of all orders</returns>
    Task<IEnumerable<Order>> GetAllOrders();
  }
}
