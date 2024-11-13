using Dapr.Workflow;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Enum;
using OrderService.Api.Exceptions;
using OrderService.Api.Workflow.Activities.CompensationActivity;
using OrderService.Api.Workflow.Activities.ExternalActivities;
using OrderService.Api.Workflow.Activities.InternalActivities;
using System.Reactive;
using OrderService.Api.Workflow.Activities;

namespace OrderService.Api.Workflow
{
  public class CreateOrderWorkflow : Workflow<OrderDto, OrderStatus>
  {
    public CreateOrderWorkflow()
    {
    }

    public override async Task<OrderStatus> RunAsync(WorkflowContext context, OrderDto order)
    {
      List<string> activityResults = new();
      try
      {
        order.Status = OrderStatus.Received;

        var validateCustomerActivityAsync = await context.CallActivityAsync<bool>(nameof(ValidateCustomerActivity), order.Customer);
        if (!validateCustomerActivityAsync)
        {
          order.Status = OrderStatus.Failed;
          activityResults.Add("ValidateCustomerActivity failed");
          throw new CustomerNotValidatedException("Customer not found");
        }
        
        activityResults.Add("ValidateCustomerActivity succeed");

        var productLines = order.ProductLines;
        var reserveCallActivityAsync = await context.CallActivityAsync<bool>(nameof(ReserveProductActivity), productLines);
        if (!reserveCallActivityAsync)
        {
          order.Status = OrderStatus.Failed;
          activityResults.Add("ReserveProductActivity failed");
          throw new ProductReservationFailedException($"Product failed to be reserved");
        }
        activityResults.Add("ReserveProductActivity succeed");

        var authorizePaymentActivity = await context.CallActivityAsync<bool>(nameof(AuthorizePaymentActivity), order);
        if (!authorizePaymentActivity)
        {
          order.Status = OrderStatus.Failed;
          activityResults.Add("AuthorizePaymentActivity failed");
          await context.CallActivityAsync(nameof(ReleaseProductActivity), productLines);
        }
        activityResults.Add("AuthorizePaymentActivity succeed");


        var confirmOrderActivity = await context.CallActivityAsync<bool>(nameof(ConfirmOrderActivity), order);
        if (!confirmOrderActivity)
        {
          activityResults.Add("ConfirmOrderActivity failed");
          await context.CallActivityAsync(nameof(ReleaseProductActivity));
        }

        activityResults.Add($"OrderCreate succeed with OrderId: {order.Id}");
        await context.CallActivityAsync(nameof(NotificationActivity), activityResults);

        return order.Status = OrderStatus.Completed;
      }
      catch (Exception ex)
      {
        await context.CallActivityAsync(nameof(NotificationActivity), activityResults);
        return OrderStatus.Failed;
      }
    }
  }
}
