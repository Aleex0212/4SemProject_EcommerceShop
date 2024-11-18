using EcommerceShop.Common.Dto;
using OrderService.Domain.Models;

namespace OrderService.Api.Mappers
{
    public class DomainMapper
  {
    internal Order MapOrderDtoToModel(OrderDto dto)
    {
      Customer customer = MapCustomerDtoToModel(dto.Customer);
      List<ProductLine>? productLines = new();
      foreach (var line in dto.ProductLines)
      {
        productLines.Add(MapProductLineDtoToModel(line));
      }
      var order = Order.Create(dto.Id, customer, productLines, dto.Status);

      return order;
    }
    internal Customer MapCustomerDtoToModel(UserDto dto)
    {
      var customer = Customer.Crate(dto.Id, dto.Name, dto.Email, dto.Phone, dto.Address);
      return customer;
    }
    internal ProductLine MapProductLineDtoToModel(ProductLineDto dto)
    {
      var product = MapProductDtoToModel(dto.Product);
      var productLine = ProductLine.Create(dto.Id, product, dto.Quantity);
      return productLine;
    }
    internal Product MapProductDtoToModel(ProductDto dto)
    {
      var product = Product.Create(dto.Id, dto.Name, dto.Price);
      return product;
    }
  }
}
