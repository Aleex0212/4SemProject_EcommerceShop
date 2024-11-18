using Dapr.Client;
using Dapr.Workflow;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;

namespace OrderService.Api.Workflow.Activities.ExternalActivities
{
  public class ReserveProductActivity : WorkflowActivity<IEnumerable<ProductLineDto>, bool>
  {
    private readonly DaprClient _daprClient;

    public ReserveProductActivity(DaprClient daprClient)
    {
      _daprClient = daprClient;
    }

    public override async Task<bool> RunAsync(WorkflowActivityContext context, IEnumerable<ProductLineDto> products)
    {
      try
      {
        var request = _daprClient.CreateInvokeMethodRequest(
          "productservice-api",
          Routes.ProductRoutes.Reserve,
          products);

        var responseJson = await _daprClient.InvokeMethodWithResponseAsync(request);
        responseJson.EnsureSuccessStatusCode();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }
  }
}
