using OrderService.Application.Interfaces;
using OrderService.Domain.Models;
using OrderService.Persistence.Db;

namespace OrderService.Persistence.Repositories
{
  public class QueryRepository : IQueryRepository
  {
    private readonly OrderData _orders;

    public QueryRepository(OrderData orders)
    {
      _orders = orders;
    }

    public IEnumerable<Order> GetAllOrders()
    {
      return _orders.Orders;
    }

    public Order GetOrder(Guid id)
    {
      return _orders.Orders.First(o => o.Id == id);
    }
  }
}
