using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Api.Controller
{
  [Route(Routes.CustomerRoutes.BaseUrl)]
  [ApiController]
  public class CustomerController : ControllerBase
  {
    [HttpPost]
    public async Task<IActionResult> Get([FromBody] CustomerDto customer)
    {
      return Ok();
      //return StatusCode(500);
    }
  }
}
