using OrderService.Domain.Models;

namespace OrderService.Application.Interfaces
{
  public interface ICommandRepository
  {
    /// <summary>
    /// Method creates an order and add to database
    /// </summary>
    /// <param name="order"></param>
    void AddOrderAsync(Order order);

    /// <summary>
    /// Method Updates and order and add to database
    /// </summary>
    /// <param name="order"></param>
    void UpdateOrder(Order order);

    /// <summary>
    /// Method removes an order and add to database
    /// </summary>
    /// <param name="order"></param>
    void DeleteOrder(Order order);
  }
}
