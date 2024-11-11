using Dapr.Workflow;
using EcommerceShop.Common.Dto;

namespace OrderService.Api.Workflow.Activities.CompensationActivity
{
  public class ReleaseProductActivity : WorkflowActivity<ProductDto, bool>
  {
    public override Task<bool> RunAsync(WorkflowActivityContext context, ProductDto input)
    {
      throw new NotImplementedException();
    }
  }
}
