using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using global::Refit;

namespace Ecommerce.FrontEnd.Interfaces.Refit
{
  public interface IAuthApi
  {
    [Post(Routes.GatewayRoutes.AuthRoutes.Login)]
    Task<string> Login([Body] LoginDto loginDto);
  }
}
