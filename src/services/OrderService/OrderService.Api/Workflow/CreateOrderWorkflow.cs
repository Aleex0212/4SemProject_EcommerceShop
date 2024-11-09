using Dapr.Workflow;
using EcommerceShop.Common.Dto;
using OrderService.Api.Workflow.Activities.ExternalActivities;
using OrderService.Domain.Models;

namespace OrderService.Api.Workflow
{
  public class CreateOrderWorkflow : Workflow<OrderDto, object?>
  {
    private readonly ILogger<CreateOrderWorkflow> _logger;

    public CreateOrderWorkflow(ILogger<CreateOrderWorkflow> logger)
    {
      _logger = logger;
    }

    //Leave empty contractor for workflow registation
    public CreateOrderWorkflow() {}

    public override async Task<object?> RunAsync(WorkflowContext context, OrderDto order)
    { 
        var products = order.ProductLinesDto.Select(p => p.ProductDto);
        await context.CallActivityAsync(nameof(ReserveProductActivity), products);
        var reservationConfirmed = await context.WaitForExternalEventAsync<bool>("ProductReservationConfirmed");
        return null;
    }
  }
}