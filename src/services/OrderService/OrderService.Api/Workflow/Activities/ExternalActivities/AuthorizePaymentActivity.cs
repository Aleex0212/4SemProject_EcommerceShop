using Dapr.Workflow;
using EcommerceShop.Common.Dto;

namespace OrderService.Api.Workflow.Activities.ExternalActivities
{
  public class AuthorizePaymentActivity : WorkflowActivity<PaymentDto, bool>
  {
    public override Task<bool> RunAsync(WorkflowActivityContext context, PaymentDto input)
    {
      throw new NotImplementedException();
    }
  }
}
