using EcommerceShop.Common.Enum;
using Microsoft.Extensions.Logging;
using OrderService.Application.Interfaces;
using OrderService.Domain;
using OrderService.Domain.Models;

namespace OrderService.Application.Services
{
  public class CommandService : ICommandService
  {
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CommandService> _logger;

    public CommandService(IOrderRepository orderRepository, IUnitOfWork unitOfWork, ILogger<CommandService> logger)
    {
      _orderRepository = orderRepository;
      _unitOfWork = unitOfWork;
      _logger = logger;
    }

    public async Task CreateOrderAsync(Order order)
    {
      try
      {
        _unitOfWork.BeginTransaction();
        await _orderRepository.AddOrderAsync(order);
        _unitOfWork.Commit();

      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message, order);
        _unitOfWork.Rollback();
      }
    }
    public void UpdateOrder(Order order)
    {
      try
      {
        _unitOfWork.BeginTransaction();
        _orderRepository.UpdateOrder(order);
        _unitOfWork.Commit();

      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message, order);
        _unitOfWork.Rollback();
      }
    }
    public void DeleteOrder(Order order)
    {
      try
      {
        _unitOfWork.BeginTransaction();
        _orderRepository.DeleteOrder(order);
        _unitOfWork.Commit();

      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message, order);
        _unitOfWork.Rollback();
      }
    }
  }
}