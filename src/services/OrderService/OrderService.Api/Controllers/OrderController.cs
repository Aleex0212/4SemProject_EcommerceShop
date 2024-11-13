using Dapr;
using Dapr.Workflow;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Queues;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Mappers;
using OrderService.Api.Workflow;
using OrderService.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace OrderService.Api.Controllers
{
  [ApiController]
  public class OrderController : ControllerBase
  {
    private readonly ILogger<OrderController> _logger;
    private readonly DaprWorkflowClient _daprWorkflowClient;
    private readonly ICommandService _commandService;
    private readonly DomainMapper _domainMapper;

    public OrderController(DaprWorkflowClient daprWorkflowClient, ILogger<OrderController> logger, ICommandService commandService, DomainMapper domainMapper)
    {
      _daprWorkflowClient = daprWorkflowClient;
      _logger = logger;
      _commandService = commandService;
      _domainMapper = domainMapper;
    }

    [HttpPost(Routes.OrderRoutes.Create)]
    [Topic(PubSub.Channel, PubSub.OrderTopic.CreateOrder)] //explicit pubsub
    [SwaggerOperation(
      Summary = "Creates a new order",
      Description = "Initiate a new order and starts the workflow",
      Tags = ["Orders"])]
    public async Task<IActionResult> Post([FromBody] OrderDto order)
    {
      var instanceId = Guid.NewGuid().ToString();
      const string workflowName = nameof(CreateOrderWorkflow);

      try
      {
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

    [HttpPost(Routes.OrderRoutes.Update)]
    [Topic(PubSub.Channel, PubSub.OrderTopic.UpdateOrder)]
    [SwaggerOperation(
      Summary = "Updates an order",
      Description = "Updates an already existing order in the database",
      Tags = ["Orders"])]
    public IActionResult Put([FromBody] OrderDto order)
    {
      try
      {
        var orderModel = _domainMapper.MapOrderDtoToModel(order);
        _commandService.UpdateOrder(orderModel);
        return Ok();
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message, $"Error updating order orderId: {order.Id}");
        return StatusCode(500);
      }
    }

    [HttpPost(Routes.OrderRoutes.Delete)]
    [Topic(PubSub.Channel, PubSub.OrderTopic.DeleteOrder)]
    [SwaggerOperation(
      Summary = "Deletes an order",
      Description = "Deletes an already existing order in the database",
      Tags = ["Orders"])]
    public IActionResult Delete([FromBody] OrderDto order)
    {
      try
      {
        var orderModel = _domainMapper.MapOrderDtoToModel(order);
        _commandService.DeleteOrder(orderModel);
        return Ok();
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message, $"Error deleting order orderId: {order.Id}");
        return StatusCode(500);
      }
    }
  }
}
