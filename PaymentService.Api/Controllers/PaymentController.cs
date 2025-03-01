﻿using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(
      Summary = "Create a product",
      Description = "Endpoint for Creating a payment",
      Tags = new[] { "Payment" })]
    public IActionResult Post([FromBody] OrderDto order)
    {
      try
      {
        return Ok($"payment successful for orderId {order.Id}");
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"Payment failed for order with Id {order.Id}");
        return StatusCode(500, $"Payment failed for order with Id {order.Id}");
      }
    }
  }
}
