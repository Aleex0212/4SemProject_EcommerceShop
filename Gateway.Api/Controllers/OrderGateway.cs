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