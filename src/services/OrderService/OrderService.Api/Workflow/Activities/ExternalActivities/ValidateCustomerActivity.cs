using Dapr.Client;
using Dapr.Workflow;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;

namespace OrderService.Api.Workflow.Activities.ExternalActivities
{
  public class ValidateCustomerActivity : WorkflowActivity<CustomerDto, bool>
  {
    private readonly DaprClient _daprClient;

    public ValidateCustomerActivity(DaprClient daprClient)
    {
      _daprClient = daprClient;
    }

    public override async Task<bool> RunAsync(WorkflowActivityContext context, CustomerDto customer)
    {
      try
      {
        var request = _daprClient.CreateInvokeMethodRequest(
          "UserService-api",
          Routes.CustomerRoutes.BaseUrl,
          customer);

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
