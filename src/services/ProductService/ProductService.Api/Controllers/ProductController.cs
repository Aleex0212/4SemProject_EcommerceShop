using Dapr;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Queues;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Mvc;
using ProductService.Api.Db;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductService.Api.Controllers
{
  [ApiController]
  public class ProductController : ControllerBase
  {
    private ProductDataDto _products;
    
    public ProductController(ProductDataDto products)
    {
      _products = products;
    }

    [HttpGet(Routes.ProductRoutes.BaseUrl)]
    [SwaggerOperation(
      Summary = "Gets all products",
      Description = "Retrieves a list of all available products",
      Tags = ["Products"])]
    public IActionResult Get()
    {
      var products = _products.Products;
      return Ok(products);
    }

    [HttpGet(Routes.ProductRoutes.BaseUrl + "/{id:guid}")]
    [SwaggerOperation(
      Summary = "Gets a product by ID",
      Description = "Retrieves the details of a product by its unique ID",
      Tags = ["Products"])]
    public IActionResult Get(Guid id)
    {
      var product = _products.Products.FirstOrDefault(p => p.Id == id);
      return Ok(product);
    }

    [Topic(PubSub.Channel, PubSub.ProductTopic.Reserve)]
    [HttpPost(Routes.ProductRoutes.Reserve)]
    public IActionResult ReserveProduct([FromBody] IEnumerable<ProductLineDto> request)
    {
      try
      {
        var dbProducts = _products.Products;
        foreach (var line in request)
        {
          var product = dbProducts.FirstOrDefault(p => p.Id == line.Product.Id);
          if (line.Quantity > product.Amount) throw new Exception("Not enough stock");
          
          dbProducts.First(p => p.Id == line.Product.Id).Amount -= line.Quantity;
        }
        _products.Products = dbProducts;  

        return Ok();
      }
      catch (Exception)
      {
        return StatusCode(500);
      }
    }

    [HttpPost(Routes.ProductRoutes.Release)]
    public IActionResult ReleaseProduct([FromBody] IEnumerable<ProductLineDto> request)
    {
      try
      {
        foreach (var line in request)
        {
          _products.Products.First(p => p.Id == line.Product.Id).Amount -= line.Quantity;
        }

        return Ok();
      }
      catch (Exception)
      {
        return StatusCode(500);
      }
    }
  }
}
