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
  [Route(Routes.GatewayRoutes.PaymmentGatewayRoutes.Payment)]
  [ApiController]
  public class PaymentGateway : ControllerBase
  {
    private readonly DaprClient _daprClient;
    private readonly ILogger<OrderGateway> _logger;

    public PaymentGateway(DaprClient daprClient, ILogger<OrderGateway> logger)
    {
      _daprClient = daprClient;
      _logger = logger;
    }

    [HttpPost]
    [Authorize(Policy = UserPolicies.CustomerPolicy)]
    [SwaggerOperation(
      Summary = "PaymentGateway",
      Description = "Gateway endpoint for calling the payment controller",
      Tags = new[] { "Gateway_Payment" })]
    public async Task<IActionResult> Post([FromBody] PaymentDto? payment)
    {
      if (payment == null) return BadRequest("Payment data is required.");
      try
      {
        await _daprClient.PublishEventAsync(PubSub.Channel, PubSub.PaymentTopic.Create, payment);
        return Ok($"Payment request sent successfully paymentId {payment.Id}.");
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"An error occurred while processing the payment. for paymentId {payment.Id}");
        return StatusCode(500, $"An error occurred while processing the paymentId. {payment.Id}");
      }
    }
  }
}
