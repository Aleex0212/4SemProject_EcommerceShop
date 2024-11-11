using EcommerceShop.Common.Enum;
using OrderService.Application.Interfaces;
using OrderService.Domain;
using OrderService.Domain.Models;

namespace OrderService.Application.Services
{
  public class CommandService : ICommandService
  {
    private readonly IOrderRepository _orderRepository;

    public CommandService(IOrderRepository orderRepository)
    {
      _orderRepository = orderRepository;
    }

    public async Task CreateOrderAsync(Guid id, Customer customer, IEnumerable<ProductLine> productLines,
      OrderStatus orderStatus)
    {
      var order = Order.Create(id, customer, productLines, orderStatus);
      await _orderRepository.AddOrderAsync(order);
    }
  }
}