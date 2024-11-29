using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Refit;

namespace Ecommerce.FrontEnd.Refit
{
    public interface IUserGatewayApi
    {
        [Get(Routes.GatewayRoutes.UserGatewayRoutes.GetByEmail)]
        Task<UserDto> GetCustomerByEmail(string email);
    }
}
