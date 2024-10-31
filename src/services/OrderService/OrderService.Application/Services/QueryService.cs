using OrderService.Application.Interfaces;
using OrderService.Domain.Models;

namespace OrderService.Application.Services
{
  public class QueryService : IQueryService
  {
    public Task<Order> GetOrder(Guid orderId, Guid customerId)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetAllOrders()
    {
      throw new NotImplementedException();
    }
  }
}
