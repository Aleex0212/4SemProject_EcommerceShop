using Dapr.Client;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Api.Controllers
{
  [Route(Routes.OrderRoutes.BaseUrl)]
  [ApiController]
  public class OrderController : ControllerBase
  {
    private readonly DaprClient _daprClient;

    public OrderController(DaprClient daprClient)
    {
      _daprClient = daprClient;
    }

    [HttpPost]
    [SwaggerOperation(
      Summary = "Creates a new order",
      Description = "Initiate a new order and starts the workflow",
      Tags = new[] { "Orders" })]
    public async Task<IActionResult> Post([FromBody] Order order)
    {
      //var correlationId = Guid.NewGuid().ToString();
      //var workflowComponentName = "dapr";
      //var workflowName = nameof(OrderWorkflow);

      //var startResponse = await _daprClient.StartWorkflowAsync(workflowComponentName, workflowName, correlationId, order);
      //return Ok(startResponse);
      return null;
    }
  }
}