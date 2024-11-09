using Dapr.Client;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Queues;
using EcommerceShop.Common.Routes;
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
    [SwaggerOperation(
      Summary = "OrderGateway",
      Description = "Calls Create Order using DaprClient")]
    public async Task<IActionResult> Post([FromBody] OrderDto order)
    {
      try
      {
        await _daprClient.PublishEventAsync(PubSub.Channel, PubSub.OrderTopic.CreateOrder, order);

        Console.WriteLine("publish event sent");
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