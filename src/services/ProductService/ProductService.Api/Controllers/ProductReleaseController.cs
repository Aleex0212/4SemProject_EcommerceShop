using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Api.Controllers
{
  [Route(Routes.ProductRoutes.Release)]
  [ApiController]
  public class ProductReleaseController : ControllerBase
  {
    [HttpPost]
    public async Task<IActionResult> Releaseproduct([FromBody] IEnumerable<ProductDto> request)
    {
      try
      {
        return Ok();
      }
      catch (Exception)
      {
        return StatusCode(500);
      }
    }
  }
}
