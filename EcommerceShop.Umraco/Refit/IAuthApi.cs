using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Refit;

namespace EcommerceShop.Umraco.Refit
{
  public interface IAuthApi
  {
    [Post(Routes.GatewayRoutes.AuthRoutes.Login)]
    Task<string> Login([Body] LoginDto loginDto);
  }
}
