using Dapr.Client;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Policies;
using EcommerceShop.Common.Queues;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Api.Controllers
{
  [Route(Routes.GatewayRoutes.OrderGatewayRoutes.Order)]
  [ApiController]
  public class OrderGateway : ControllerBase
  {
    private readonly DaprClient _daprClient;
    private readonly ILogger<OrderGateway> _logger;

    public OrderGateway(DaprClient daprClient, ILogger<OrderGateway> logger)
    {
      _daprClient = daprClient;
      _logger = logger;
    }

    [HttpPost]
    [Authorize(Policy = UserPolicies.CustomerPolicy)]
    [SwaggerOperation(
      Summary = "OrderGateway",
      Description = "Calls Create Order using DaprClient and starts workflow",
      Tags = new[] { "Gateway_Order" })]
    public async Task<IActionResult> Post([FromBody] OrderDto order)
    {
      try
      {
        await _daprClient.PublishEventAsync(PubSub.Channel, PubSub.OrderTopic.CreateOrder, order);
        return Ok($"Order with OrderId:  {order.Id} Successfully created and workflow initialised.");
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error starting OrderGateway");
        return StatusCode(500, "An error occurred while processing the order.");
      }
    }

    [HttpGet]
    [Authorize(Policy = UserPolicies.AdminPolicy)]
    [SwaggerOperation(
      Summary = "Get all orders",
      Description = "Retrieves a collection of all orders",
      Tags = new[] { "Gateway_Order" })]
    public async Task<IActionResult> Get()
    {
      try
      {
        var request = _daprClient.CreateInvokeMethodRequest(
          httpMethod: HttpMethod.Get,
          "orderservice-api",
          Routes.OrderRoutes.Get);

        var responseJson = await _daprClient.InvokeMethodWithResponseAsync(request);
        responseJson.EnsureSuccessStatusCode();

        var orders = await responseJson.Content.ReadFromJsonAsync<IEnumerable<OrderDto>>();
        return Ok(orders);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Error retrieving orders: {ex.Message}");
        return StatusCode(500, "Internal server error while fetching products");
      }
    }

    [HttpGet("{id}")]
    [Authorize(Policy = UserPolicies.AdminPolicy)]
    [SwaggerOperation(
      Summary = "Get order by ID",
      Description = "Retrieves a single order by its ID",
      Tags = new[] { "Gateway_Order" })]
    public async Task<IActionResult> Get(Guid id)
    {
      try
      {
        var request = _daprClient.CreateInvokeMethodRequest(
          httpMethod: HttpMethod.Get,
          appId: "orderservice-api",
          methodName: $"{Routes.OrderRoutes.GetById.Replace("{id}", id.ToString())}");

        var responseJson = await _daprClient.InvokeMethodWithResponseAsync(request);
        responseJson.EnsureSuccessStatusCode();

        var order = await responseJson.Content.ReadFromJsonAsync<OrderDto>();

        if (order == null) return NotFound($"Product with ID {id} was not found");

        return Ok(order);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Error retrieving order with ID {id}: {ex.Message}");
        return StatusCode(500, "Internal server error while fetching the product");
      }
    }

    [HttpGet(Routes.GatewayRoutes.OrderGatewayRoutes.GetByEmail)]
    [Authorize(Policy = UserPolicies.CustomerPolicy)]
    [SwaggerOperation(
      Summary = "Get order by customer CustomerEmail",
      Description = "Retrieves a collection of orders by customer CustomerEmail",
      Tags = new[] { "Gateway_Order" })]
    public async Task<IActionResult> Get(string customerEmail)
    {
      try
      {
        var request = _daprClient.CreateInvokeMethodRequest(
          httpMethod: HttpMethod.Get,
          appId: "orderservice-api",
          methodName: $"{Routes.OrderRoutes.GetByEmail.Replace("{customerEmail}", customerEmail)}");

        var responseJson = await _daprClient.InvokeMethodWithResponseAsync(request);
        responseJson.EnsureSuccessStatusCode();

        var order = await responseJson.Content.ReadFromJsonAsync<IEnumerable<OrderDto>>();

        if (order == null) return NotFound($"order with customer CustomerEmail {customerEmail} was not found");

        return Ok(order);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Error retrieving order for customer CustomerEmail {customerEmail}: {ex.Message}");
        return StatusCode(500, "Internal server error while fetching the product");
      }
    }

    [HttpPut]
    [Authorize(Policy = UserPolicies.AdminPolicy)]
    [SwaggerOperation(
      Summary = "OrderGateway",
      Description = "Calls Update Order using DaprClient",
      Tags = new[] { "Gateway_Order" })]
    public async Task<IActionResult> Put([FromBody] OrderDto order)
    {
      try
      {
        await _daprClient.PublishEventAsync(PubSub.Channel, PubSub.OrderTopic.UpdateOrder, order);
        return Ok($"Update for order with OrderId {order.Id} initialised");
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error starting OrderGateway");
        return StatusCode(500, "An error occurred while updating the order.");
      }
    }

    [HttpDelete]
    [Authorize(Policy = UserPolicies.AdminPolicy)]
    [SwaggerOperation(
      Summary = "OrderGateway",
      Description = "Calls Delete Order using DaprClient",
      Tags = new[] { "Gateway_Order" })]
    public async Task<IActionResult> Delete([FromBody] OrderDto order)
    {
      try
      {
        await _daprClient.PublishEventAsync(PubSub.Channel, PubSub.OrderTopic.DeleteOrder, order);
        return Ok($"delete of order with orderId {order.Id} initialised");
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Error while attempting to deleting order {order.Id}.");
        return StatusCode(500, "An error occurred while deleting the order.");
      }
    }
  }
}