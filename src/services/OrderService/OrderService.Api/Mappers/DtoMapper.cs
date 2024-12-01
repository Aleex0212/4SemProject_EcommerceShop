using EcommerceShop.Common.Dto;
using OrderService.Domain.Models;

namespace OrderService.Api.Mappers
{
  public class DtoMapper
  {
    internal OrderDto MapOrderToDto(Order order)
    {
      return new OrderDto()
      {
        Customer = MapCustomerToDto(order.Customer),
        ProductLines = MapProductLinesToDto(order.ProductLines).ToList(),
        Id = order.Id,
        Status = order.Status,
        TotalPrice = order.TotalPrice,
      };
    }

    internal UserDto MapCustomerToDto(Customer customer)
    {
      return new UserDto()
      {
        Address = customer.Address,
        Email = customer.Email,
        Id = customer.Id,
        Name = customer.Name,
        Phone = customer.Phone,
        Password = ""
      };
    }

    internal IEnumerable<ProductLineDto> MapProductLinesToDto(IEnumerable<ProductLine> productLine)
    {
      var productlines = new List<ProductLineDto>();
      foreach (var line in productLine)
      {
        var tmp = new ProductLineDto()
        {
          Id = line.Id,
          Product = MapProductToDto(line.Product),
          Quantity = line.Quantity
        };
        productlines.Add(tmp);
      }
      return productlines;
    }

    internal ProductDto MapProductToDto(Product product)
    {
      return new ProductDto()
      {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
        Amount = 0
      };
    }
  }
}
