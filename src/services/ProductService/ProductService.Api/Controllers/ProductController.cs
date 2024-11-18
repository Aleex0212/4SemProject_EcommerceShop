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
    private readonly ILogger<ProductController> _logger;
    private ProductDataDto _products;

    public ProductController(ProductDataDto products, ILogger<ProductController> logger)
    {
      _products = products;
      _logger = logger;
    }

    [HttpGet(Routes.ProductRoutes.Create)]
    [SwaggerOperation(
      Summary = "Create a new product",
      Description = "Endpoint for creating a new single product",
      Tags = ["Products"])]
    public IActionResult Post([FromBody] ProductDto product)
    {
      try
      {
        _products.Products.Add(product);
        return Ok($"product successfully created with productId : {product.Id}");
      }
      catch (Exception)
      {
        _logger.LogError(500, $"Could not create product {product.Id}");
        return StatusCode(500, $"something went wrong during creating product Id : {product.Id}");
      }
    }

    [HttpPut(Routes.ProductRoutes.Update)]
    [Topic(PubSub.Channel, PubSub.ProductTopic.Update)]
    [SwaggerOperation(
      Summary = "Updates an existing product",
      Description = "Updates a product based on the provided ProductDto",
      Tags = new[] { "Products" })]
    public IActionResult Put([FromBody] ProductDto product)
    {
      try
      {
        var existingProduct = _products.Products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct == null) return NotFound($"Product with Id : {product.Id} was not found");

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Amount = product.Amount;

        return Ok($"Product successfully updated for productId {product.Id}");
      }
      catch (Exception)
      {
        _logger.LogError(500, $"something went wrong during update of productId {product.Id}");
        return StatusCode(500, $"something went wrong during update of productId {product.Id}");
      }
    }

    [HttpGet(Routes.ProductRoutes.Get)]
    [SwaggerOperation(
      Summary = "Gets all products",
      Description = "Retrieves a list of all available products",
      Tags = ["Products"])]
    public IActionResult Get()
    {
      try
      {
        var products = _products.Products;
        return Ok(products);
      }
      catch (Exception)
      {
        _logger.LogError(500, "Something went wrong during fetching of products");
        return StatusCode(500, "Something went wrong during fetching of products");
      }
    }

    [HttpGet(Routes.ProductRoutes.GetById)]
    [SwaggerOperation(
      Summary = "Gets a product by unique ID",
      Description = "Retrieves the details of a product by its unique ID",
      Tags = ["Products"])]
    public IActionResult Get(Guid id)
    {
      try
      {
        var product = _products.Products.FirstOrDefault(p => p.Id == id);
        if (product is null) return StatusCode(404, $"product with id {product.Id} was not found");
        return Ok(product);
      }
      catch (Exception)
      {
        _logger.LogError(500, $"something went wrong during fetching productId : {id}");
        return StatusCode(500, $"something went wrong during fetching productId : {id}");
      }
    }

    [Topic(PubSub.Channel, PubSub.ProductTopic.Reserve)]
    [SwaggerOperation(
      Summary = "Reserves a product",
      Description = "Endpoint for reserving a product",
      Tags = ["Products"])]
    [HttpPost(Routes.ProductRoutes.Reserve)]
    public IActionResult ReserveProduct([FromBody] IEnumerable<ProductLineDto> productLines)
    {
      try
      {
        var dbProducts = _products.Products;
        foreach (var line in productLines)
        {
          var product = dbProducts.FirstOrDefault(p => p.Id == line.Product.Id);
          if (product == null) return StatusCode(404, $"Product with ID {line.Product.Id} not found");

          if (line.Quantity > product.Amount) return StatusCode(400, "Not enough stock");

          product.Amount -= line.Quantity;
        }
        _products.Products = dbProducts;

        return Ok(new { message = "Product reservation successful", reservedProducts = productLines });
      }
      catch (Exception ex)
      {
        _logger.LogError(500, $"An error occurred while reserving product: {ex.Message}");
        return StatusCode(500, $"An error occurred while reserving product: {ex.Message}");
      }
    }

    [HttpPost(Routes.ProductRoutes.Release)]
    [SwaggerOperation(
      Summary = "Releases a product",
      Description = "Endpoint for releasing a product back to stock",
      Tags = new[] { "Products" })]
    public IActionResult ReleaseProduct([FromBody] IEnumerable<ProductLineDto> productLine)
    {
      try
      {
        foreach (var line in productLine)
        {
          var product = _products.Products.FirstOrDefault(p => p.Id == line.Product.Id);
          if (product == null) return NotFound($"Product with ID {line.Product.Id} not found");

          product.Amount += line.Quantity;
        }

        return Ok(new { message = "Product released successfully", releasedProducts = productLine });
      }
      catch (Exception ex)
      {
        _logger.LogError($"An error occurred while releasing product: {ex.Message}");
        return StatusCode(500, new { message = $"An error occurred while releasing product: {ex.Message}" });
      }
    }
  }
}
