using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Mvc;

namespace PaymentService.Api.Controllers
{
  [Route(Routes.PaymentRoutes.Payment)]
  [ApiController]
  public class PaymentController : ControllerBase
  {
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(ILogger<PaymentController> logger)
    {
      _logger = logger;
    }

    [HttpPost]
    public IActionResult Post([FromBody] PaymentDto payment)
    {
      try
      {
        return Ok($"payment successful for paymentId {payment.Id}");
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Payment failed for payment with Id {payment.Id}");
        return StatusCode(500, $"Payment failed for payment with Id {payment.Id}");
      }
    }
  }
}
