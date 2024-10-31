using EcommerceShop.Common.Dto;
using Microsoft.Extensions.Logging;
using ProductService.Application.Interfaces;

namespace ProductService.Application.Services.Query
{
  public class QueryService : IQueryService
  {
    private readonly IRepository _repository;
    private readonly ILogger<IQueryService> _logger;

    public QueryService(IRepository repository, ILogger<IQueryService> logger)
    {
      _repository = repository;
      _logger = logger;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
      var products = await _repository.GetAllProductsAsync();
      return products.Select(p => new ProductDto
      {
        Id = p.Id,
        Name = p.Name,
        Quantity = p.Quantity
      }).ToList();
    }

    public async Task<ProductDto> GetProductByIdAsync(Guid productId)
    {
      var product = await _repository.GetProductByIdAsync(productId);
      if (product == null) throw new KeyNotFoundException($"Product not found. ProductId:{productId}");
      return new ProductDto
      {
        Id = product.Id,
        Name = product.Name,
        Quantity = product.Quantity
      };
    }
  }
}