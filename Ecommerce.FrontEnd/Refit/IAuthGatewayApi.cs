using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Refit;

namespace Ecommerce.FrontEnd.Refit
{
    public interface IAuthGatewayApi
    {
        [Post(Routes.GatewayRoutes.AuthRoutes.Login)]
        Task<string> Login([Body] LoginDto loginDto);
    }
}
