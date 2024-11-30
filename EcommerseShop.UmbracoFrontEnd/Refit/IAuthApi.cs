using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Refit;

namespace EcommerceShop.UmbracoFrontEnd.Refit
{
  public interface IAuthApi
  {
    [Post(Routes.GatewayRoutes.AuthRoutes.Login)]
    Task<string> Login([Body] LoginDto loginDto);
  }
}
