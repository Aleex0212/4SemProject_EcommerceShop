using OrderService.Domain.Models;

namespace OrderService.Application.Services
{
  public interface IOrderRepository
  {
    Task AddOrderAsync(Order order);
  }
}
