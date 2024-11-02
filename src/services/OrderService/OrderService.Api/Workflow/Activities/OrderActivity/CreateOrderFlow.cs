using Dapr.Workflow;
using OrderService.Api.Workflow.Activities.ExternalActivities;
using OrderService.Domain.Models;

namespace OrderService.Api.Workflow.Activities.OrderActivity
{
    public class CreateOrderFlow : Workflow<Order, OrderResult>
    {
        public override async Task<OrderResult> RunAsync(WorkflowContext context, Order order)
        {
            var newOrder = new OrderResult
            {
                Order = order,
                Status = OrderStatus.Received,
                Message = $"Received order {order.Id} from {order.Id}",
                TimeStamp = DateTime.Now,
            };

            //await context.CallActivityAsync(NotificationActivity), new Notification(
            //  $"Initializing Order with Id :{order.Id}, " +
            //  $"CustomerId :{order.Customer.Id} " +
            //  $"and products :{order.ProductLines}");


            await context.CallActivityAsync(nameof(ReserveProductActivity), order);

            return newOrder;
        }
    }
}
