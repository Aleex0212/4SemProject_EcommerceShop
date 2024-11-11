using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Mvc;

namespace PaymentService.Api.Controllers
{
  [Route(Routes.PaymentRoutes.BaseUrl)]
  [ApiController]
  public class PaymentController : ControllerBase
  {
    [HttpPost]
    public async Task<IActionResult> Get([FromBody] OrderDto customer)
    {
      return Ok();
      //return StatusCode(500);
    }
  }
}
