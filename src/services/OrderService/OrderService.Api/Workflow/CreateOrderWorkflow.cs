using Dapr.Workflow;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Enum;
using OrderService.Api.Exceptions;
using OrderService.Api.Workflow.Activities.CompensationActivity;
using OrderService.Api.Workflow.Activities.ExternalActivities;
using OrderService.Api.Workflow.Activities.InternalActivities;

namespace OrderService.Api.Workflow
{
  public class CreateOrderWorkflow : Workflow<OrderDto, OrderStatus>
  {
    public CreateOrderWorkflow()
    {
    }

    public override async Task<OrderStatus> RunAsync(WorkflowContext context, OrderDto order)
    {
      try
      {
        order.Status = OrderStatus.Received;

        var validateCustomerActivityAsync = await context.CallActivityAsync<bool>(nameof(ValidateCustomerActivity), order.Customer);
        if (!validateCustomerActivityAsync)
        {
          order.Status = OrderStatus.Failed;
          throw new CustomerNotValidatedException("Customer not found");
        }

        var productLines= order.ProductLines;
        var reserveCallActivityAsync = await context.CallActivityAsync<bool>(nameof(ReserveProductActivity), productLines);
        if (!reserveCallActivityAsync)
        {
          order.Status = OrderStatus.Failed;
          throw new ProductReservationFailedException($"Product failed to be reserved");
        }

        var authorizePaymentActivity = await context.CallActivityAsync<bool>(nameof(AuthorizePaymentActivity), order);
        if (!authorizePaymentActivity)
        {
          order.Status = OrderStatus.Failed;
          await context.CallActivityAsync(nameof(ReleaseProductActivity), productLines);
        }

        var confirmOrderActivity = await context.CallActivityAsync<bool>(nameof(ConfirmOrderActivity), order);
        if (!confirmOrderActivity)
        {
          await context.CallActivityAsync(nameof(ReleaseProductActivity));
        }

        return order.Status = OrderStatus.Completed;
      }
      catch (Exception ex)
      {
        return OrderStatus.Failed;
      }
    }
  }
}
