using System.Threading.Channels;
using Dapr;
using Dapr.Client;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Queues;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Workflow.Activities.OrderActivity;
using OrderService.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Api.Controllers
{
    [Route(Routes.OrderRoutes.BaseUrl)]
  [ApiController]
  public class OrderController : ControllerBase
  {
    private readonly ILogger<OrderController> _logger;
    private readonly DaprClient _daprClient;

    public OrderController(DaprClient daprClient, ILogger<OrderController> logger)
    {
      _daprClient = daprClient;
      _logger = logger;
    }

    [HttpPost]
    [Topic(OrderChannel.Channel, OrderChannel.Topics.CreateOrder)]
    [SwaggerOperation(
      Summary = "Creates a new order",
      Description = "Initiate a new order and starts the workflow",
      Tags = new[] { "Orders" })]
    public async Task<IActionResult> Post([FromBody] OrderDto order)
    {
      var correlationId = Guid.NewGuid().ToString();
      var workflowComponentName = "dapr";
      var workflowName = nameof(CreateOrderFlow);

      try
      {
        var startResponse =
          await _daprClient.StartWorkflowAsync(workflowComponentName, workflowName, correlationId, order);
        return Ok(startResponse);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message, $"Error starting Workflow {workflowName}");
        return StatusCode(500);
      }
    }
  }
}
