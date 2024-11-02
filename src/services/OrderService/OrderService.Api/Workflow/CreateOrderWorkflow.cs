using Dapr.Workflow;
using OrderService.Api.Workflow.Activities.ExternalActivities;
using OrderService.Domain.Models;

namespace OrderService.Api.Workflow
{
  public class CreateOrderWorkflow : Workflow<Order, OrderResult>
  {
    private readonly ILogger<CreateOrderWorkflow> _logger;

    public CreateOrderWorkflow(ILogger<CreateOrderWorkflow> logger)
    {
      _logger = logger;
    }

    //Leave empty contractor for workflow registation
    public CreateOrderWorkflow() {}

    public override async Task<OrderResult> RunAsync(WorkflowContext context, Order order)
    {
      var newOrder = new OrderResult
      {
        Order = order,
        Status = OrderStatus.Received,
        Message = $"Received order {order.Id} from customer {order.Customer.Id}",
        TimeStamp = DateTime.Now,
      };

      _logger.LogInformation($"Starting workflow for order ID {order.Id}", order.Status);

      try
      {
        await context.CallActivityAsync(nameof(ReserveProductActivity), order);
        newOrder.Status = OrderStatus.Received;
        _logger.LogInformation($"Product reservation activity started for order ID {order.Id}", order.Status);

        var reservationConfirmed = await context.WaitForExternalEventAsync<bool>("ProductReservationConfirmed");

        if (reservationConfirmed) newOrder.Status = OrderStatus.Processing;
        else
        {
          newOrder.Status = OrderStatus.Failed;
          newOrder.Message = $"Product reservation failed for order {order.Id}.";
          _logger.LogError($"Product reservation failed for order {order.Id}.");
        }
      }

      catch (Exception ex)
      {
        newOrder.Status = OrderStatus.Failed;
        _logger.LogError(ex, "Error occurred during workflow for order ID {OrderId}", order.Id);
        newOrder.Message = $"Failed to process order {order.Id} due to an error.";
      }

      return newOrder;
    }
  }
}