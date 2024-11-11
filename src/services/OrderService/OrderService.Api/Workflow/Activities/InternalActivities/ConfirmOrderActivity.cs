using Dapr.Workflow;
using EcommerceShop.Common.Dto;
using OrderService.Api.Mappers;
using OrderService.Application.Interfaces;

namespace OrderService.Api.Workflow.Activities.InternalActivities
{
  public class ConfirmOrderActivity : WorkflowActivity<OrderDto, bool>
  {
    private readonly ICommandService _commandService;
    private readonly DomainMapper _domainMapper;

    public ConfirmOrderActivity(ICommandService commandService, DomainMapper domainMapper)
    {
      _commandService = commandService;
      _domainMapper = domainMapper;
    }

    public override async Task<bool> RunAsync(WorkflowActivityContext context, OrderDto order)
    {
      var domainModel = _domainMapper.MapOrderDtoToModel(order);
      await _commandService.CreateOrderAsync(domainModel.Id, domainModel.Customer, domainModel.ProductLines, domainModel.Status);
      return true;
    }
  }
}
