using OrderService.Application.Interfaces;
using OrderService.Domain.Models;

namespace OrderService.Application.Services
{
  public class CommandService : ICommandService
  {
    public Task CreateOrder(Order order)
    {
      throw new NotImplementedException();
    }

    public Task DeleteOrder(Guid orderId)
    {
      throw new NotImplementedException();
    }
  }
}
