using OrderService.Application.Interfaces;
using OrderService.Domain.Models;

namespace OrderService.Application.Services
{
  public class CommandService : ICommandService
  {
    public Task CreateOrderAsync(Order order)
    {
      throw new NotImplementedException();
    }

    public Task DeleteOrderAsync(Guid orderId)
    {
      throw new NotImplementedException();
    }
  }
}
