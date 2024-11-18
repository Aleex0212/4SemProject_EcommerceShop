using Dapr.Client;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Policies;
using EcommerceShop.Common.Queues;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.Api.Controllers
{
  [Route(Routes.GatewayRoutes.ProductGatewayRoutes.Product)]
  [ApiController]
  public class ProductGateway : ControllerBase
  {
    private readonly DaprClient _daprClient;
    private readonly ILogger<ProductGateway> _logger;

    public ProductGateway(DaprClient daprClient, ILogger<ProductGateway> logger)
    {
      _daprClient = daprClient;
      _logger = logger;
    }

    [HttpPost]
    [Authorize(Policy = UserPolicies.AdminPolicy)]
    [SwaggerOperation(
      Summary = "Creates a product",
      Description = "Endpoint for creating a product",
      Tags = new[] { "Gateway_Product" })]
    public async Task<IActionResult> Post([FromBody] ProductDto product)
    {
      try
      {
        await _daprClient.PublishEventAsync(PubSub.Channel, PubSub.ProductTopic.Create, product);
        return Ok($"Product created successfully productId {product.Id}");
      }
      catch (Exception ex)
      {
        _logger.LogError($"Error creating product with productId : {product.Id} - {ex.Message}");
        return StatusCode(500, $"Internal server error while creating product with productId {product.Id}");
      }
    }

    [HttpPut]
    [Authorize(Policy = UserPolicies.AdminPolicy)]
    [SwaggerOperation(
      Summary = "Updates an existing product",
      Description = "Endpoint for updating a product",
      Tags = new[] { "Gateway_Product" })]
    public async Task<IActionResult> Put([FromBody] ProductDto product)
    {
      try
      {
        await _daprClient.PublishEventAsync(PubSub.Channel, PubSub.ProductTopic.Update, product);
        return Ok($"Product updated successfully productId : {product.Id}");
      }
      catch (Exception ex)
      {
        _logger.LogError($"Error updating product with productId : {product.Id} - {ex.Message}");
        return StatusCode(500, "Internal server error while updating product");
      }
    }

    [HttpGet]
    [Authorize(Policy = UserPolicies.CustomerPolicy)]
    [SwaggerOperation(
      Summary = "Get all products",
      Description = "Retrieves a collection of all products",
      Tags = new[] { "Gateway_Product" })]
    public async Task<IActionResult> Get()
    {
      try
      {
        var request = _daprClient.CreateInvokeMethodRequest(
          httpMethod: HttpMethod.Get, 
          "productservice-api",
          Routes.ProductRoutes.Get);

        var responseJson = await _daprClient.InvokeMethodWithResponseAsync(request);
        responseJson.EnsureSuccessStatusCode();

        var products = await responseJson.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
        return Ok(products);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Error retrieving products: {ex.Message}");
        return StatusCode(500, "Internal server error while fetching products");
      }
    }

    [HttpGet("{id}")]
    [Authorize(Policy = UserPolicies.CustomerPolicy)]
    [SwaggerOperation(
      Summary = "Get Product by ID",
      Description = "Retrieves a single product by its ID",
      Tags = new[] { "Gateway_Product" })]
    public async Task<IActionResult> Get(Guid id)
    {
      try
      {
        var request = _daprClient.CreateInvokeMethodRequest(
          httpMethod: HttpMethod.Get,
          appId: "productservice-api",
          methodName: $"{Routes.ProductRoutes.GetById.Replace("{id}", id.ToString())}");

        var responseJson = await _daprClient.InvokeMethodWithResponseAsync(request);
        responseJson.EnsureSuccessStatusCode();

        var product = await responseJson.Content.ReadFromJsonAsync<ProductDto>();

        if (product == null) return NotFound($"Product with ID {id} was not found");
        
        return Ok(product);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Error retrieving product with ID {id}: {ex.Message}");
        return StatusCode(500, "Internal server error while fetching the product");
      }
    }
  }
}
