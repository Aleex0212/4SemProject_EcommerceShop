using Microsoft.Extensions.Logging;
using OrderService.Application.Interfaces;
using OrderService.Domain.Models;

namespace OrderService.Application.Services
{
  public class CommandService : ICommandService
  {
    private readonly ICommandRepository _orderRepository;
    private readonly ILogger<CommandService> _logger;

    public CommandService(ICommandRepository orderRepository, ILogger<CommandService> logger)
    {
      _orderRepository = orderRepository;
      _logger = logger;
    }

    public void CreateOrderAsync(Order order)
    {
      try
      {
        _orderRepository.AddOrderAsync(order);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message, order);
      }
    }
    public void UpdateOrder(Order order)
    {
      try
      {
        _orderRepository.UpdateOrder(order);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message, order);
      }
    }
    public void DeleteOrder(Order order)
    {
      try
      {
        _orderRepository.DeleteOrder(order);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message, order);
      }
    }
  }
}