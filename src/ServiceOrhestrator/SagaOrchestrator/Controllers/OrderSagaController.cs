using Dapr.Client;
using EcommerceShop.Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SagaOrchestrator.SignalR.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SagaOrchestrator.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrderSagaController : ControllerBase
  {
    private readonly DaprClient _daprClient;
    private readonly IHubContext<StatusUpdateHub, ISignalRHub> _signalRHub;

    public OrderSagaController(DaprClient daprClient, IHubContext<StatusUpdateHub, ISignalRHub> signalRHub)
    {
      _daprClient = daprClient;
      _signalRHub = signalRHub;
    }

    [HttpPost("create-order")]
    [SwaggerOperation(
        Summary = "Creates a new order",
        Description = "Reserves a product for a customer and publishes an event for order processing",
        Tags = new[] { "Orders" })]
    [SwaggerResponse(200, "Order processing started.")]
    [SwaggerResponse(404, "Product or Customer not found.")]
    public async Task<IActionResult> CreateOrder([FromBody] ProductReservationRequestDto request)
    {
      try
      {
        var product = await GetProductAsync(request.ProductId);
        var customerId = await GetCustomerIdAsync(request.CustomerId);

        if (product == null)
        {
          await _signalRHub.Clients.All.SendStatusUpdateAsync($"Product not found. ProductId:{product?.Id}");
          return NotFound($"Product not found.ProductId:{product?.Id}");
        }
        if (string.IsNullOrEmpty(customerId.ToString()))
        {
          await _signalRHub.Clients.All.SendStatusUpdateAsync($"Customer not found.CustomerId:{customerId}");
          return NotFound($"Customer not found.CustomerId:{customerId}");
        }

        var reservationMessage = new ReserveProductDto
        {
          OrderId = Guid.NewGuid(),
          CustomerId = customerId,
          ProductId = request.ProductId,
          Quantity = request.Quantity
        };

        await _daprClient.PublishEventAsync("pubsub", "product-reservation", reservationMessage);
        await _signalRHub.Clients.All.SendStatusUpdateAsync($"Product Id : {reservationMessage.ProductId} reserved for Customer Id: {customerId}.");


        return Ok("Order processing started.");
      }
      catch (Exception ex)
      {
        await _signalRHub.Clients.All.SendStatusUpdateAsync($"Order processing failed: {ex.Message}");
        return StatusCode(500, "Internal server error");
      }
    }

    private async Task<ProductDto> GetProductAsync(Guid productId)
    {
      var product = await _daprClient.InvokeMethodAsync<ProductDto>("ProductService", $"api/products/{productId}");
      return product;
    }

    private async Task<Guid> GetCustomerIdAsync(Guid customerId)
    {
      var customer = await _daprClient.InvokeMethodAsync<Guid>("CustomerService", $"api/customers/{customerId}");
      return customer;
    }
  }
}
