using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Refit;

namespace EcommerceShop.UmbracoFrontEnd.Refit
{
  public interface IUserApi
  {
    [Get(Routes.GatewayRoutes.UserGatewayRoutes.User + "/{email}")]
    Task<UserDto> GetCustomerByEmail(string email);
  }
}
