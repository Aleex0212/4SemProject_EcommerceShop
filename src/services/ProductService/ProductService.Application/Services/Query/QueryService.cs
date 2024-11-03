using EcommerceShop.Common.Dto;
using Microsoft.Extensions.Logging;
using ProductService.Application.Interfaces;

namespace ProductService.Application.Services.Query
{
  public class QueryService : IQueryService
  {
    private readonly IRepository _repository;
    private readonly ILogger<IQueryService> _logger;
    private readonly IProductMapper _productMapper;

    public QueryService(IRepository repository, ILogger<IQueryService> logger, IProductMapper productMapper)
    {
      _repository = repository;
      _logger = logger;
      _productMapper = productMapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
      var products = await _repository.GetAllProductsAsync();
      var productDtoList = products.Select(product => _productMapper.MapToDtoModel(product)).ToList();
      return productDtoList;
    }

    public async Task<ProductDto> GetProductByIdAsync(Guid productId)
    {
      var product = await _repository.GetProductByIdAsync(productId);
      if (product == null)
      {
        _logger.LogWarning($"Product not found. ProductId: {product.Id}");
        throw new KeyNotFoundException($"Product not found. ProductId: {productId}");
      }

      var productDto = _productMapper.MapToDtoModel(product);
      return productDto;
    }
  }
}