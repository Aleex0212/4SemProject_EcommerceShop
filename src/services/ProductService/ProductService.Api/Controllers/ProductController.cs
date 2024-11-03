using Dapr;
using EcommerceShop.Common.IntegrationEvents;
using EcommerceShop.Common.Queues;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductService.Api.Controllers
{
  [Route(Routes.ProductRoutes.BaseUrl)]
  [ApiController]
  public class ProductController : ControllerBase
  {
    private readonly ICommandService _commandService;
    private readonly IQueryService _queryService;

    public ProductController(ICommandService commandService, IQueryService queryService)
    {
      _commandService = commandService;
      _queryService = queryService;
    }


    [Topic(PubSub.Base, PubSub.ProductTopic.Reserve)]
    [HttpPut("reserve")]
    public async Task<IActionResult> ReserveProduct([FromBody] ReserveProductDto request)
    {
      try
      {
        await _commandService.ReserveProductAsync(request);
        return Ok("Product reservation processed successfully.");
      }
      catch (Exception ex)
      {
        return StatusCode(500);
        return null;
      }
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all products",
        Description = "Retrieves a list of all available products",
        Tags = new[] { "Products" })]
    public async Task<IActionResult> Get()
    {
      var products = await _queryService.GetAllProductsAsync();
      if (products == null || !products.Any())
        return NotFound("No products found.");

      return Ok(products);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Gets a product by ID",
        Description = "Retrieves the details of a product by its unique ID",
        Tags = new[] { "Products" })]
    public async Task<IActionResult> Get(Guid id)
    {
      var product = await _queryService.GetProductByIdAsync(id);
      if (product == null)
        return NotFound($"Product with ID {id} not found.");

      return Ok(product);
    }
  }
}
