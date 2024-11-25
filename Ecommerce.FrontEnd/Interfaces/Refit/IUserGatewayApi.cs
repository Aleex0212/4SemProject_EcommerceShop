
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Refit;

namespace Ecommerce.FrontEnd.Interfaces.Refit
{
  public interface IUserGatewayApi
  {
    [Get(Routes.GatewayRoutes.UserGatewayRoutes.User + "/{email}")]
    Task<UserDto> GetCustomerByEmail(string email);
  }
}
