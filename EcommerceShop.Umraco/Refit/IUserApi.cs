using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Refit;

namespace EcommerceShop.Umraco.Refit
{
  public interface IUserApi
  {
    [Get(Routes.GatewayRoutes.UserGatewayRoutes.User + "/{email}")]
    Task<UserDto> GetCustomerByEmail(string email);
  }
}
