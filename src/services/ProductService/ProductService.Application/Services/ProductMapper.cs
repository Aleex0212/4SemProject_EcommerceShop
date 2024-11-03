using EcommerceShop.Common.Dto;
using EcommerceShop.Common.IntegrationEvents;
using ProductService.Application.Interfaces;
using ProductService.Domain.Models;

namespace ProductService.Application.Services
{
  public class ProductMapper : IProductMapper
  {
    public Product MapToDomainModel(ProductDto productDto)
    {
      return new Product(Guid.NewGuid(), productDto.Name, productDto.Price, productDto.Quantity);
    }

    public Product MapToDomainModel(ReserveProductDto reserveProductDto, Product product)
    {
      foreach (var productLineDto in reserveProductDto.Products.Where(productLineDto => product.Id == productLineDto.ProductDto.Id))
      {
        product.Quantity = Product.Reserve(product.Quantity, productLineDto.Quantity);
      }
      return product;
    }

    public ProductDto MapToDtoModel(Product product)
    {
      return new ProductDto
      {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,  
        Quantity = product.Quantity
      };
    }
  }
}