using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Refit;

namespace Ecommerce.FrontEnd.Refit
{
    public interface IProductGatewayApi
    {
        [Get(Routes.GatewayRoutes.ProductGatewayRoutes.Product)]
        Task<IEnumerable<ProductDto>?> GetProductsAsync([Header("Authorization")] string bearerToken);

        [Post(Routes.GatewayRoutes.ProductGatewayRoutes.Product)]
        Task<string> CreateProductAsync([Body] ProductDto productDto, [Header("Authorization")] string bearerToken);
    }
}
