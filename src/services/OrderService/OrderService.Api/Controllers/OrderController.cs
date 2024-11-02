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
    private readonly DaprWorkflowClient _workflowDaprClient;

    public OrderController(DaprWorkflowClient workflowDaprClient, ILogger<OrderController> logger)
    {
      _workflowDaprClient = workflowDaprClient;
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
      var instanceId = Guid.NewGuid().ToString();
      const string workflowName = nameof(CreateOrderWorkflow);

      try
      {
        var startResponse =
          await _workflowDaprClient.ScheduleNewWorkflowAsync(workflowName, instanceId, order);

        var response = _workflowDaprClient.WaitForWorkflowCompletionAsync(startResponse);
        return Ok(response.Result);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message, $"Error starting Workflow {workflowName}");
        return StatusCode(500);
      }
    }
  }
}
