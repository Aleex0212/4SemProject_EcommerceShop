using Dapr.Workflow;
using EcommerceShop.Common.Dto;
using OrderService.Domain.Models;

namespace OrderService.Api.Workflow.Activities.OrderActivity
{
    public class CreateOrderActivity : IWorkflowActivity
    {
      public Task<object?> RunAsync(WorkflowActivityContext context, object? input)
      {
        throw new NotImplementedException();
      }
      public Type InputType { get; }
      public Type OutputType { get; }
    }
}
