using Dapr.Workflow;
using EcommerceShop.Common.Dto;
using OrderService.Api.Mappers;
using OrderService.Application.Interfaces;
using System.Windows.Input;

namespace OrderService.Api.Workflow.Activities.InternalActivities
{
  public class ConfirmOrderActivity : WorkflowActivity<OrderDto, bool>
  {
    private readonly ICommandService _commandService;
    private readonly DomainMapper _domainMapper;

    public ConfirmOrderActivity(ICommandService commandService)
    {
      _commandService = commandService;
    }

    public override async Task<bool> RunAsync(WorkflowActivityContext context, OrderDto input)
    {
      var order = _domainMapper.MapOrderDtoToModel(input);
      await _commandService.CreateOrderAsync(order);
      throw new NotImplementedException();
    }
  }
}
