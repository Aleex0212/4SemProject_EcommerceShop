using EcommerceShop.Common.Enum;
using OrderService.Domain;
using OrderService.Domain.Models;

namespace OrderService.Application.Interfaces
{
  public interface ICommandService
  {
    /// <summary>
    /// Create an order.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="customer"></param>
    /// <param name="productLines"></param>
    /// <param name="orderStatus"></param>
    /// <param name="order">
    Task CreateOrderAsync(Guid id, Customer customer, IEnumerable<ProductLine> productLines, OrderStatus orderStatus);
  }
}
