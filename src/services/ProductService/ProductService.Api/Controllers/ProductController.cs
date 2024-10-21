using Dapr;
using EcommerceShop.Common.Dto;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Interfaces;

namespace ProductService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Topic("pubsub", "product-reservation")]
        [HttpPost("reserve")]
        public async Task<IActionResult> ReserveProduct([FromBody] ReserveProductMessageDto request)
        {
            try
            {
                await _productService.ReserveProductAsync(request.ProductId, request.Quantity);
                return Ok("Product reservation processed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error processing product reservation: {ex.Message} for product {request.ProductId}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            if (products == null || !products.Any())
                return NotFound("No products found.");

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            return Ok(product);
        }
    }
}