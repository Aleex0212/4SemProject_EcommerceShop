﻿using Dapr.Client;
using Dapr.Workflow;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;

namespace OrderService.Api.Workflow.Activities.ExternalActivities
{
  public class ReserveProductActivity : WorkflowActivity<ProductDto, bool>
  {
    private readonly DaprClient _daprClient;

    public ReserveProductActivity(DaprClient daprClient)
    {
      _daprClient = daprClient;
    }

    public override async Task<bool> RunAsync(WorkflowActivityContext context, ProductDto product)
    {
      try
      {
        var request = _daprClient.CreateInvokeMethodRequest(
          "productservice-api",
          Routes.ProductRoutes.Reserve,
          product);

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
