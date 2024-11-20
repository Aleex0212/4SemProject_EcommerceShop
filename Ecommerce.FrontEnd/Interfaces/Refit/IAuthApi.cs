using global::Refit;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;

namespace Ecommerce.FrontEnd.Interfaces.Refit
{
  public interface IAuthApi
  {
    [Post(Routes.GatewayRoutes.AuthRoutes.Login)]
    Task<string> Login([Body] LoginDto loginDto);
  }
}
