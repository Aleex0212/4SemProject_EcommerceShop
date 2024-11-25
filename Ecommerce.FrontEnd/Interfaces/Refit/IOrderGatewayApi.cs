using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Refit;

namespace Ecommerce.FrontEnd.Interfaces.Refit
{
  public interface IOrderGatewayApi
  {
    [Post(Routes.GatewayRoutes.OrderGatewayRoutes.Order)]
    Task CreateOrderAsync([Body] OrderDto order, [Header("Authorization")] string bearerToken);
  }
}
