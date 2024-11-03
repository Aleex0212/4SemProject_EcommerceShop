using EcommerceShop.Common.Dto;
using EcommerceShop.Common.IntegrationEvents;
using ProductService.Domain.Models;

namespace ProductService.Application.Interfaces
{
  public interface IProductMapper
  {
    /// <summary>
    /// Maps the ProductDto to Domain Product
    /// </summary>
    /// <param name="productDto"></param>
    /// <returns></returns>
    Product MapToDomainModel(ProductDto productDto);

    /// <summary>
    /// Maps the ReserveProductDto to Domain Product
    /// </summary>
    /// <param name="reserveProductDto"></param>
    /// <param name="product"></param>
    /// <returns></returns>
    Product MapToDomainModel(ReserveProductDto reserveProductDto, Product product);

    /// <summary>
    /// Maps from DomainModel To Dto
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public ProductDto MapToDtoModel(Product product);
  }
}