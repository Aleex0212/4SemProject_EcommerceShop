using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Refit;

namespace EcommerseShop.UmbracoFrontEnd.Refit
{
  public interface IOrderApi
  {
    [Post(Routes.GatewayRoutes.OrderGatewayRoutes.Order)]
    Task CreateOrderAsync([Body] OrderDto order, [Header("Authorization")] string bearerToken);
  }
}
