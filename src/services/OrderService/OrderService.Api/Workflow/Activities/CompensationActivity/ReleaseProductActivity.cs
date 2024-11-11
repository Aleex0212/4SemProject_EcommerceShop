using Dapr.Client;
using Dapr.Workflow;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;

namespace OrderService.Api.Workflow.Activities.CompensationActivity
{
  public class ReleaseProductActivity : WorkflowActivity<IEnumerable<ProductDto>, bool>
  {
    private readonly DaprClient _daprClient;

    public ReleaseProductActivity(DaprClient daprClient)
    {
      _daprClient = daprClient;
    }
    public override async Task<bool> RunAsync(WorkflowActivityContext context, IEnumerable<ProductDto> input)
    {
      try
      {
        var request = _daprClient.CreateInvokeMethodRequest(
          "productservice-api",
          Routes.ProductRoutes.Release,
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
