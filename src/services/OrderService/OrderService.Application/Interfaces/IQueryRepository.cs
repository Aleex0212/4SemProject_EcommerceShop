using OrderService.Domain.Models;

namespace OrderService.Application.Interfaces
{
  public interface IQueryRepository
  {
    /// <summary>
    /// Method retrieves all order from database
    /// </summary>
    /// <returns>IEnumerable<Order />
    /// </returns>
    IEnumerable<Order> GetAllOrders();

    /// <summary>
    /// Method get a single order by its id from database
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Order GetOrder(Guid id);
  }
}
