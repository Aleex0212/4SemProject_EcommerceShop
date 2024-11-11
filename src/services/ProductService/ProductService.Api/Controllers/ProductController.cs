using Dapr;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Queues;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductService.Api.Controllers
{
  [Route(Routes.ProductRoutes.BaseUrl)]
  [ApiController]
  public class ProductController : ControllerBase
  {

    [Topic(PubSub.Channel, PubSub.ProductTopic.Reserve)]
    [HttpPost]
    public async Task<IActionResult> ReserveProduct([FromBody] IEnumerable<ProductDto> request)
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

    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all products",
        Description = "Retrieves a list of all available products",
        Tags = ["Products"])]
    public async Task<IActionResult> Get()
    {
      return Ok();
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Gets a product by ID",
        Description = "Retrieves the details of a product by its unique ID",
        Tags = ["Products"])]
    public async Task<IActionResult> Get(Guid id)
    {
      return Ok();
    }
  }
}
