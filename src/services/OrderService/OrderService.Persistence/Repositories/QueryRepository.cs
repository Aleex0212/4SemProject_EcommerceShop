using OrderService.Application.Interfaces;
using OrderService.Domain.Models;
using OrderService.Persistence.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
