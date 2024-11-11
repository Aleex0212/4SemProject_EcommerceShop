using Dapr.Client;
using Dapr.Workflow;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using OrderService.Domain.Models;

namespace OrderService.Api.Workflow.Activities.ExternalActivities
{
  public class AuthorizePaymentActivity : WorkflowActivity<OrderDto, bool>
  {
    private readonly DaprClient _daprClient;

    public AuthorizePaymentActivity(DaprClient daprClient)
    {
      _daprClient = daprClient;
    }

    public override async Task<bool> RunAsync(WorkflowActivityContext context, OrderDto input)
    {
      try
      {
        var request = _daprClient.CreateInvokeMethodRequest(
          "paymentservice-api",
          Routes.PaymentRoutes.BaseUrl,
          input);

        var responseJson = await _daprClient.InvokeMethodWithResponseAsync(request);
        responseJson.EnsureSuccessStatusCode();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }
  }
}
