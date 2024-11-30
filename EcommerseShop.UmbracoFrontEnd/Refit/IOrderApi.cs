using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Refit;

namespace EcommerseShop.UmbracoFrontEnd.Refit
{
  public interface IOrderApi
  {
    [Post(Routes.GatewayRoutes.OrderGatewayRoutes.Order)]
    //Do not use - Does not serialize correct
    Task CreateOrderAsync([Body] OrderDto order, [Header("Authorization")] string bearerToken);

    [Get(Routes.GatewayRoutes.OrderGatewayRoutes.GetByEmail)]
    Task<IEnumerable<OrderDto>> GetOrdersByCustomerEmail(string customerEmail, [Header("Authorization")] string bearerToken);
  }
}
