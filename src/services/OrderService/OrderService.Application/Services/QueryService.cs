using OrderService.Application.Interfaces;
using OrderService.Domain.Models;

namespace OrderService.Application.Services
{
  public class QueryService : IQueryService
  {
    private readonly IQueryRepository _repository;

    public QueryService(IQueryRepository repository)
    {
      _repository = repository;
    }

    public IEnumerable<Order> GetAllOrders()
    {
      return _repository.GetAllOrders();
    }

    public Order GetOrder(Guid orderId)
    {
      return _repository.GetOrder(orderId);
    }
  }
}
