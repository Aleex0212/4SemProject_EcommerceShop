using Dapr;
using Dapr.Client;
using Dapr.Workflow;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Queues;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Workflow;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Api.Controllers
{
    [Route(Routes.OrderRoutes.BaseUrl)]
  [ApiController]
  public class OrderController : ControllerBase
  {
    private readonly ILogger<OrderController> _logger;
    private readonly DaprWorkflowClient _daprWorkflowClient;

    public OrderController(DaprWorkflowClient daprWorkflowClient, ILogger<OrderController> logger)
    {
      _daprWorkflowClient = daprWorkflowClient;
      _logger = logger;
    }

    [HttpPost]
    [Topic(PubSub.Base, PubSub.OrderTopic.CreateOrder)]
    [SwaggerOperation(
      Summary = "Creates a new order",
      Description = "Initiate a new order and starts the workflow",
      Tags = new[] { "Orders" })]
    public async Task<IActionResult> Post([FromBody] OrderDto order)
    {
      var instanceId = Guid.NewGuid().ToString();
      const string workflowName = nameof(CreateOrderWorkflow);

      try
      {
        Console.WriteLine("subscribe event received");
        var startResponse =
          await _daprWorkflowClient.ScheduleNewWorkflowAsync(workflowName, instanceId, order);

        var workflowStateAsync = await _daprWorkflowClient.WaitForWorkflowCompletionAsync(startResponse);
        return Ok(workflowStateAsync);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message, $"Error starting Workflow {workflowName}");
        return StatusCode(500);
      }
    }
  }
}
