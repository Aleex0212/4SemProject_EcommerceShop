using Dapr.Workflow;
using EcommerceShop.Common.Dto;

namespace OrderService.Api.Workflow.Activities.CompensationActivity
{
  public class CancelOrderActivity : WorkflowActivity<OrderDto, bool>
  {
    public override Task<bool> RunAsync(WorkflowActivityContext context, OrderDto input)
    {
      throw new NotImplementedException();
    }
  }
}
