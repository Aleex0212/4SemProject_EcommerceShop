using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Refit;

namespace Ecommerce.FrontEnd.Refit
{
    public interface IProductGatewayApi
    {
        [Get(Routes.GatewayRoutes.ProductGatewayRoutes.Product)]
        Task<IEnumerable<ProductDto>?> GetProductsAsync([Header("Authorization")] string bearerToken);
    }
}
