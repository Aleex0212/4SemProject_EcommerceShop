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
    public IActionResult Post([FromBody] OrderDto customer)
    {
      return Ok();
    }
  }
}
