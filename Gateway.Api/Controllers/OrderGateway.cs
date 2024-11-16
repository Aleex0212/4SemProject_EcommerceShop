using Dapr.Client;
using EcommerceShop.Common.Dto;
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

    [Authorize(Roles = "customer")]
    [HttpPost]
    [SwaggerOperation(
      Summary = "OrderGateway",
      Description = "Calls Create Order using DaprClient")]
    public async Task<IActionResult> Post([FromBody] OrderDto order)
    {
      try
      {
        await _daprClient.PublishEventAsync(PubSub.Channel, PubSub.OrderTopic.CreateOrder, order);
        return Ok();
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message, $"Error starting OrderGateway {ex.Message}");
        return StatusCode(500);
      }
    }

    [HttpPut]
    [Authorize(Roles = "admin")]
    [SwaggerOperation(
      Summary = "OrderGateway",
      Description = "Calls Update Order using DaprClient")]
    public async Task<IActionResult> Put([FromBody] OrderDto order)
    {
      try
      {
        await _daprClient.PublishEventAsync(PubSub.Channel, PubSub.OrderTopic.UpdateOrder, order);
        return Ok();
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message, $"Error starting OrderGateway {ex.Message}");
        return StatusCode(500);
      }
    }

    [HttpDelete]
    [Authorize (Roles = "admin")]
    [SwaggerOperation(
      Summary = "OrderGateway",
      Description = "Calls Delete Order using DaprClient")]
    public async Task<IActionResult> Delete([FromBody] OrderDto order)
    {
      try
      {
        await _daprClient.PublishEventAsync(PubSub.Channel, PubSub.OrderTopic.DeleteOrder, order);
        return Ok();
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message, $"Error starting OrderGateway {ex.Message}");
        return StatusCode(500);
      }
    }
  }
}