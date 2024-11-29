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
    private readonly IQueryService _queryService;
    private readonly DomainMapper _domainMapper;
    private readonly DtoMapper _dtoMapper;

    public OrderController(DaprWorkflowClient daprWorkflowClient, ILogger<OrderController> logger,
      ICommandService commandService, DomainMapper domainMapper, IQueryService queryService, DtoMapper dtoMapper)
    {
      _daprWorkflowClient = daprWorkflowClient;
      _logger = logger;
      _commandService = commandService;
      _domainMapper = domainMapper;
      _queryService = queryService;
      _dtoMapper = dtoMapper;
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

    [HttpGet(Routes.OrderRoutes.Get)]
    [SwaggerOperation(
  Summary = "Gets all products",
  Description = "Retrieves a list of all available products",
  Tags = ["Orders"])]
    public IActionResult Get()
    {
      try
      {
        var orders = _queryService.GetAllOrders();
        if (!orders.Any()) return NoContent();

        return Ok(orders);
      }
      catch (Exception)
      {
        _logger.LogError(500, "Something went wrong during fetching of orders");
        return StatusCode(500, "Something went wrong during fetching of orders");
      }
    }

    [HttpGet(Routes.OrderRoutes.GetById)]
    [SwaggerOperation(
      Summary = "Gets an order by unique ID",
      Description = "Retrieves the details of an order by its unique ID",
      Tags = ["Orders"])]
    public IActionResult Get(Guid id)
    {
      try
      {
        var order = _queryService.GetOrder(id);
        if (order == null) return NoContent();

        return Ok(order);
      }
      catch (Exception)
      {
        _logger.LogError(500, $"something went wrong during fetching productId : {id}");
        return StatusCode(500, $"something went wrong during fetching productId : {id}");
      }
    }

    [HttpGet(Routes.OrderRoutes.GetByEmail)]
    [SwaggerOperation(
      Summary = "Gets an collection of orders by customer email",
      Description = "Retrieves all orders by customer email",
      Tags = ["Orders"])]
    public IActionResult Get(string customerEmail)
    {
      try
      {
        var orderDtos = new List<OrderDto>();

        var orders = _queryService.GetOrdersByCustomerEmail(customerEmail);
        if (!orders.Any()) return NoContent();

        foreach (var order in orders)
        {
          var orderDto = _dtoMapper.MapOrderToDto(order);
          orderDtos.Add(orderDto);
        }
        return Ok(orderDtos);
      }
      catch (Exception)
      {
        _logger.LogError(500, $"something went wrong during fetching order by customerEmail : {customerEmail}");
        return StatusCode(500, $"something went wrong during fetching order by customerEmail : {customerEmail}");
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
