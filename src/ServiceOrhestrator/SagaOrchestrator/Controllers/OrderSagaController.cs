using Dapr.Client;
using EcommerceShop.Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SagaOrchestrator.SignalR.Interfaces;

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
        public async Task<IActionResult> CreateOrder([FromBody] ProductReservationRequestDto request)
        {
            try
            {
                var product = await GetProductAsync(request.ProductId);
                var customerId = await GetCustomerIdAsync(request.CustomerId);

                if (product == null)
                {
                    await _signalRHub.Clients.All.SendStatusUpdateAsync("Product not found.");
                    return NotFound("Product not found.");
                }
                if (string.IsNullOrEmpty(customerId.ToString()))
                {
                    await _signalRHub.Clients.All.SendStatusUpdateAsync("Customer not found.");
                    return NotFound("Customer not found.");
                }

                var reservationMessage = new ReserveProductMessageDto
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
            return await _daprClient.InvokeMethodAsync<ProductDto>("ProductService", $"api/products/{productId}");
        }

        private async Task<Guid> GetCustomerIdAsync(Guid customerId)
        {
            return await _daprClient.InvokeMethodAsync<Guid>("CustomerService", $"api/customers/{customerId}");
        }
    }
}
